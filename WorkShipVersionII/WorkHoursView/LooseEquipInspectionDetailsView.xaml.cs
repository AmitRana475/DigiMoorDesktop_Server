using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for LooseEquipInspectionDetailsView.xaml
    /// </summary>
    public partial class LooseEquipInspectionDetailsView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public LooseEquipInspectionDetailsView()
        {
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            comboLooseEtype.SelectedIndex = 0;

            dpRecDate.DisplayDateEnd = DateTime.Now.Date;
            // bindLooseEType();
            // GetMooringInspection();
        }

        private void bindLooseEType()
        {
            try
            {
                SqlDataAdapter adp1 = new SqlDataAdapter("select * from looseetype", sc.con);
                DataSet dt1 = new DataSet();
                adp1.Fill(dt1);
                if (dt1.Tables[0].Rows.Count > 0)
                {

                    comboLooseEtype.ItemsSource = dt1.Tables[0].DefaultView;
                    comboLooseEtype.DisplayMemberPath = dt1.Tables[0].Columns["LooseEquipmentType"].ToString();
                    comboLooseEtype.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();

                }
            }
            catch { }
        }

       

        //private void GetMooringInspection()
        //{

        //    // DateTime dd = DateTime.Now.Date;

        //    //LooseEquipInspectionDetailsViewModel.loadUserAccess.Clear();
           

        //    string qry = "LooseEquipInspection";
        //    SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);

        //    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    sda.SelectCommand.Parameters.AddWithValue("@id", 1);
        //    sda.SelectCommand.Parameters.AddWithValue("@table_name", "Joining Shackle");
        //    DataSet datatbl = new DataSet();
        //    sda.Fill(datatbl);

        //    DataGridRow row = MooringLooseEInspectionGrid.ItemContainerGenerator.ContainerFromIndex
        //        (MooringLooseEInspectionGrid.SelectedIndex) as DataGridRow;
        //    //var i = 0; 
        //    ////EDIT
          
        //    for (int i = 0; i < datatbl.Tables[0].Rows.Count; i++)
        //    {

        //        MooringLooseEInspectionGrid.ItemsSource = datatbl.Tables[0].DefaultView;

        //        //TextBox test = ((ContentPresenter)(MooringLooseEInspectionGrid.Columns[i].GetCellContent(row))).Content as TextBox;

        //        LooseEquipInspectionDetailsViewModel.loadUserAccess.Add(new MooringLooseEquipInspectionClass()
        //        {
        //            InspectDate = DateTime.Now,
        //            LooseETypeId = Convert.ToInt32(datatbl.Tables[0].Rows[i]["looseetypeid"]),
        //            looseequipmenttype = datatbl.Tables[0].Rows[i]["looseequipmenttype"].ToString(),


        //            //Condition = test.ToString(),
        //            //    Remarks = ""
        //        });

              
               

        //    }
            
          
        //}

        //private void comboLooseEtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        LooseEquipInspectionDetailsViewModel.loadUserAccess.Clear();
        //        int id = Convert.ToInt32( comboLooseEtype.SelectedValue);

        //        SqlDataAdapter adp1 = new SqlDataAdapter("select * from LooseEType where id=" + id + "", sc.con);
        //        DataTable dtt = new DataTable();
        //        adp1.Fill(dtt);
        //        string tablename = dtt.Rows[0][1].ToString();
        //        //string test=comboLooseEtype.sele

        //        //LooseEquipInspectionDetailsViewModel.GetMooringInspection(id, tablename);

        //        ComboBoxItem text = comboLooseEtype.SelectedItem as ComboBoxItem;
        //        //var text = ((ComboBoxItem)comboLooseEtype.SelectedItem).Content.ToString();

        //        SqlDataAdapter adp = new SqlDataAdapter("LooseEquipInspection", sc.con);
        //        adp.SelectCommand.Parameters.AddWithValue("@id", id);
        //        adp.SelectCommand.Parameters.AddWithValue("@table_name", tablename);
        //        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        DataSet dt = new DataSet();
        //        adp.Fill(dt);

                

        //            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
        //        {
        //            MooringLooseEInspectionGrid.ItemsSource = dt.Tables[0].DefaultView;

        //            TextBlock x = MooringLooseEInspectionGrid.Columns[i].GetCellContent(MooringLooseEInspectionGrid.Items[0]) as TextBlock;

        //            //object item = MooringLooseEInspectionGrid.SelectedItem;
        //            //string ID = (MooringLooseEInspectionGrid.SelectedCells[i].Column.GetCellContent(item) as TextBlock).Text;



        //            LooseEquipInspectionDetailsViewModel.loadUserAccess.Add(new MooringLooseEquipInspectionClass()
        //            {
        //                InspectDate = DateTime.Now,
        //                LooseETypeId = Convert.ToInt32(dt.Tables[0].Rows[i]["looseetypeid"]),
        //                looseequipmenttype = dt.Tables[0].Rows[i]["looseequipmenttype"].ToString(),
        //                Condition = "",
        //                Remarks = ""
        //            });

        //        }

        //        if (dt.Tables[0].Rows.Count <= 0)
        //        {
        //            MooringLooseEInspectionGrid.ItemsSource = null;
        //        }

        //    }
        //    catch { }


        //}

       

        private void comboLooseEtype_DropDownClosed_1(object sender, EventArgs e)
        {
            if(comboLooseEtype.Text != "--Select--")
            {
                btnSave.IsEnabled = true;
            }
        }
        public static List<PhotoListLoose> imagesavelist = new List<PhotoListLoose>();
        public static List<PhotoListLoose1> imagesavelist1 = new List<PhotoListLoose1>();
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;

                MooringLooseEquipInspectionClass obj = clickedButton.DataContext as MooringLooseEquipInspectionClass;

                if (clickedButton.Content == "Delete Image")
                {
                    //SqlDataAdapter adp = new SqlDataAdapter("update MooringLooseEquipInspection set Image1=null,Photo1=null where ID= " + obj.Id + "", sc.con);
                    SqlDataAdapter adp = new SqlDataAdapter("UpdatetMooringLooseEquipInspection", sc.con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@Id", obj.Id);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);


                    imagesavelist.ForEach(x => {
                        if (x.Id == obj.Id && x.LooseETypeId == obj.LooseETypeId)
                        {
                            x.photo1 = null;
                            x.imagename1 = null;

                            if (File.Exists(x.Path1))
                            {
                                File.Delete(x.Path1);
                            }
                        }
                    });

                    clickedButton.Content = "Browse";

                    MessageBox.Show("Image deleted successfully", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MooringLooseEquipInspectionClass cls = new MooringLooseEquipInspectionClass();

                            string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";

                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages";
               // string ServerName = StaticHelper.ServerName;
                //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
               // string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + DateTime.Now.ToString("dd -MMM-yyyy") + "";

                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }

                System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                            fldlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                            fldlg.ShowDialog();
                {
                    string strName = fldlg.SafeFileName;
                    string imageName = fldlg.FileName;


                    Random randon = new Random();
                    int num = randon.Next(10000);


                    string fileName = System.IO.Path.GetFileName(strName);


                    string fileExtention = System.IO.Path.GetExtension(imageName);
                    //string fileName = System.IO.Path.GetFileName(fldlg.FileName);
                    var withoutextnsn = System.IO.Path.GetFileNameWithoutExtension(fileName) + num;

                    fileName = withoutextnsn + fileExtention;

                    mypath = mypath + "\\" + fileName;


                    var size = new FileInfo(fldlg.FileName).Length / 1024;

                    if (size > 200)
                    {
                        MessageBox.Show("Image Size can not exceed 200Kb !", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {

                        byte[] ss = System.IO.File.ReadAllBytes(imageName);
                        LooseEquipInspectionDetailsViewModel._mooringInspact.Photo1 = ss;

                        LooseEquipInspectionDetailsViewModel._mooringInspact.Image1 = strName;

                        PhotoListLoose myphoto = new PhotoListLoose()
                        {
                            Id=obj.Id,
                            LooseETypeId = obj.LooseETypeId,
                            Number = obj.Number,
                            photo1 = null,
                            imagename1 = fileName,
                            Path1 = mypath

                        };
                        imagesavelist.Add(myphoto);

                        //string fileName = System.IO.Path.GetFileName(strName);
                        //mypath = mypath + "\\" + fileName;

                        clickedButton.Content = "Delete Image";
                        File.Copy(fldlg.FileName, mypath);
                    }
                }
                fldlg = null;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnBrowse_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;

                MooringLooseEquipInspectionClass obj = clickedButton.DataContext as MooringLooseEquipInspectionClass;

                if (clickedButton.Content == "Delete Image")
                {
                    //SqlDataAdapter adp = new SqlDataAdapter("update MooringLooseEquipInspection set Image1=null,Photo1=null where ID= " + obj.Id + "", sc.con);
                    //DataTable dt = new DataTable();
                    //adp.Fill(dt);

                    SqlDataAdapter adp = new SqlDataAdapter("UpdatetMooringLooseEquipInspection", sc.con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@Id", obj.Id);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    imagesavelist1.ForEach(x => {
                        if (x.Id == obj.Id && x.LooseETypeId == obj.LooseETypeId)
                        {
                            x.photo2 = null;
                            x.imagename2 = null;

                            if (File.Exists(x.Path2))
                            {
                                File.Delete(x.Path2);
                            }
                        }
                    });

                    clickedButton.Content = "Browse";

                    MessageBox.Show("Image deleted successfully", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MooringLooseEquipInspectionClass cls = new MooringLooseEquipInspectionClass();

                            //string ServerName = StaticHelper.ServerName;
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + DateTime.Now.ToString("dd -MMM-yyyy") + "";

                            string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                           // string mypath = @"C:\DigiMoorDB_Backup\InspectionImages";

                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }

                System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                            fldlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                            fldlg.ShowDialog();
                {
                    string strName = fldlg.SafeFileName;
                    string imageName = fldlg.FileName;

                    Random randon = new Random();
                    int num = randon.Next(10000);


                    string fileName = System.IO.Path.GetFileName(strName);


                    string fileExtention = System.IO.Path.GetExtension(imageName);
                    //string fileName = System.IO.Path.GetFileName(fldlg.FileName);
                    var withoutextnsn = System.IO.Path.GetFileNameWithoutExtension(fileName) + num;

                    fileName = withoutextnsn + fileExtention;

                    mypath = mypath + "\\" + fileName;


                    var size = new FileInfo(fldlg.FileName).Length / 1024;

                    if (size > 200)
                    {
                        MessageBox.Show("Image Size can not exceed 200Kb !", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {

                        byte[] ss = System.IO.File.ReadAllBytes(imageName);
                        LooseEquipInspectionDetailsViewModel._mooringInspact.Photo2 = ss;

                        LooseEquipInspectionDetailsViewModel._mooringInspact.Image2 = strName;

                        PhotoListLoose1 myphoto = new PhotoListLoose1()
                        {
                            Id=obj.Id,
                            LooseETypeId = obj.LooseETypeId,
                            Number = obj.Number,
                            photo2 = null,
                            imagename2 = fileName,
                            Path2 = mypath

                        };
                        imagesavelist1.Add(myphoto);


                        //string fileName = System.IO.Path.GetFileName(strName);
                        //mypath = mypath + "\\" + fileName;
                        clickedButton.Content = "Delete Image";
                        File.Copy(fldlg.FileName, mypath);
                    }
                }
                fldlg = null;
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class PhotoListLoose
    {
        public int Id { get; set; }
        public int LooseETypeId { get; set; }
        public string Number { get; set; }
        public byte[] photo1 { get; set; }
        public string imagename1 { get; set; }

        public string Path1 { get; set; }

    }

    public class PhotoListLoose1
    {
        public int Id { get; set; }
        public int LooseETypeId { get; set; }
        public string Number { get; set; }

        public byte[] photo2 { get; set; }
        public string imagename2 { get; set; }
        public string Path2 { get; set; }
    }
}
