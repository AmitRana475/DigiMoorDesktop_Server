using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for MooringRopeInspectionView.xaml
    /// </summary>
    public partial class MooringTailInspectionView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public MooringTailInspectionView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
        }



        private void cboExternal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox)
                {
                    ComboBox comBox = (ComboBox)e.OriginalSource;
                    // get DataGridRow which checkBox exits.
                    MooringTailInspectionView rowData = comBox.DataContext as MooringTailInspectionView;
                    DataGridRow dataGridRow = DataGridRow.GetRowContainingElement(comBox);
                    string text = ((ComboBoxItem)comBox.SelectedItem).Content.ToString();

                    DataGridCellInfo cells = MooringRopeInspectionGrid.SelectedCells[1];

                    MooringRopeInspectionClass item = cells.Item as MooringRopeInspectionClass;
                    object columnName = cells.Item.GetType().GetProperty("InternalRating_A").GetValue(item, null);

                    int firstvalue = Convert.ToInt32(text);
                    int secondvalue = Convert.ToInt32(columnName);
                    decimal finalvalue = (firstvalue + secondvalue);
                    finalvalue = finalvalue / 2;

                    finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                    TextBlock txts = MooringRopeInspectionGrid.SelectedCells[9].Column.GetCellContent(dataGridRow.Item) as TextBlock;

                    int Score = Convert.ToInt32(text) * 200;
                    txts.Text = finalvalue.ToString();
                    //MooringRopeInspectionViewModel._mooringInspact.AverageRating_A = Convert.ToInt32(finalvalue);
                }
            }
            catch { }
        }

        private void cboInternal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox)
                {
                    ComboBox comBox = (ComboBox)e.OriginalSource;
                    // get DataGridRow which checkBox exits.
                    MooringTailInspectionView rowData = comBox.DataContext as MooringTailInspectionView;
                    DataGridRow dataGridRow = DataGridRow.GetRowContainingElement(comBox);
                    string text = ((ComboBoxItem)comBox.SelectedItem).Content.ToString();

                    DataGridCellInfo cells = MooringRopeInspectionGrid.SelectedCells[1];

                    MooringRopeInspectionClass item = cells.Item as MooringRopeInspectionClass;
                    object columnName = cells.Item.GetType().GetProperty("ExternalRating_A").GetValue(item, null);

                    int firstvalue = Convert.ToInt32(text);
                    int secondvalue = Convert.ToInt32(columnName);
                    decimal finalvalue = (firstvalue + secondvalue);
                    finalvalue = finalvalue / 2;

                    finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                    TextBlock txts = MooringRopeInspectionGrid.SelectedCells[9].Column.GetCellContent(dataGridRow.Item) as TextBlock;

                    int Score = Convert.ToInt32(text) * 200;
                    txts.Text = finalvalue.ToString();
                    // MooringRopeInspectionViewModel._mooringInspact.AverageRating_A = Convert.ToInt32(finalvalue);
                }
            }
            catch { }
        }



        private void cboExternalB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox)
                {
                    ComboBox comBox = (ComboBox)e.OriginalSource;
                    // get DataGridRow which checkBox exits.
                    MooringTailInspectionView rowData = comBox.DataContext as MooringTailInspectionView;
                    DataGridRow dataGridRow = DataGridRow.GetRowContainingElement(comBox);
                    string text = ((ComboBoxItem)comBox.SelectedItem).Content.ToString();

                    DataGridCellInfo cells = MooringRopeInspectionGrid.SelectedCells[1];

                    MooringRopeInspectionClass item = cells.Item as MooringRopeInspectionClass;
                    object columnName = cells.Item.GetType().GetProperty("InternalRating_B").GetValue(item, null);

                    int firstvalue = Convert.ToInt32(text);
                    int secondvalue = Convert.ToInt32(columnName);
                    decimal finalvalue = (firstvalue + secondvalue);
                    finalvalue = finalvalue / 2;

                    finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                    TextBlock txts = MooringRopeInspectionGrid.SelectedCells[16].Column.GetCellContent(dataGridRow.Item) as TextBlock;


                    txts.Text = finalvalue.ToString();
                    // MooringRopeInspectionViewModel._mooringInspact.AverageRating_B = Convert.ToInt32(finalvalue);
                }
            }
            catch { }
        }

        private void cboInternalB_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox)
                {
                    ComboBox comBox = (ComboBox)e.OriginalSource;
                    // get DataGridRow which checkBox exits.
                    MooringTailInspectionView rowData = comBox.DataContext as MooringTailInspectionView;
                    DataGridRow dataGridRow = DataGridRow.GetRowContainingElement(comBox);
                    string text = ((ComboBoxItem)comBox.SelectedItem).Content.ToString();

                    DataGridCellInfo cells = MooringRopeInspectionGrid.SelectedCells[1];

                    MooringRopeInspectionClass item = cells.Item as MooringRopeInspectionClass;
                    object columnName = cells.Item.GetType().GetProperty("ExternalRating_B").GetValue(item, null);

                    int firstvalue = Convert.ToInt32(text);
                    int secondvalue = Convert.ToInt32(columnName);
                    decimal finalvalue = (firstvalue + secondvalue);
                    finalvalue = finalvalue / 2;

                    finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                    TextBlock txts = MooringRopeInspectionGrid.SelectedCells[16].Column.GetCellContent(dataGridRow.Item) as TextBlock;


                    txts.Text = finalvalue.ToString();
                    //MooringRopeInspectionViewModel._mooringInspact.AverageRating_B = Convert.ToInt32(finalvalue);

                }
            }
            catch { }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void TextBox_PreviewTextInput_3(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

        }

        private void TextBox_PreviewTextInput_4(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void TextBox_PreviewTextInput_5(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

        }

        private void TextBox_PreviewTextInput_6(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

        }

        private void TextBox_PreviewTextInput_7(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;

                MooringRopeInspectionClass obj = clickedButton.DataContext as MooringRopeInspectionClass;

               

                if (clickedButton.Content == "Delete Image")
                {
                    SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image1=null,Photo1=null where ID= " + obj.Id + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    imagesavelist.ForEach(x => {
                        if (x.Id == obj.Id && x.RopeId == obj.RopeId)
                        {
                            x.photo1 = null;
                            x.imagename1 = null;


                        }
                    });

                    clickedButton.Content = "Browse";

                    MessageBox.Show("Image deleted successfully", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                MooringRopeInspectionClass cls = new MooringRopeInspectionClass();

                            // string ServerName = StaticHelper.ServerName;
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + DateTime.Now.ToString("dd -MMM-yyyy") + "";

                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\" + DateTime.Now.ToString("dd-MMM-yyyy") + "";

                            string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\";

                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }


                System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                            fldlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                            fldlg.ShowDialog();
                {
                    try
                    {
                        if (imagesavelist != null && imagesavelist.Count != 0)
                        {
                            var itemToRemove = imagesavelist.Single(r => r.RopeId == obj.RopeId);
                            imagesavelist.Remove(itemToRemove);

                        }
                    }
                    catch { }

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
                        MessageBox.Show("Image Size can not exceed 200Kb !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {

                        byte[] ss = System.IO.File.ReadAllBytes(imageName);
                        MooringTailInspectionViewModel._mooringInspact.Photo1 = ss;

                        MooringTailInspectionViewModel._mooringInspact.Image1 = strName;

                        PhotoList myphoto = new PhotoList()
                        {
                            Id = obj.Id,
                            RopeId = obj.RopeId,
                            WinchId = obj.WinchId,
                            photo1 = null,
                            imagename1 = fileName

                        };
                        imagesavelist.Add(myphoto);



                        MooringTailInspectionViewModel.loadUserAccess.ToList().ForEach(x =>
                        {
                            if (x.Id == myphoto.Id)
                            {
                                x.Photo11 = "Hidden";
                                x.Showbutton1 = "Visible";
                                x.Photo1 = null;
                                x.Image1 = fileName;
                                x.ButtonContent1 = "Delete Image";

                                //StaticHelper. = mw.RopeId;
                                //photoimglist1 = null;
                            };
                        });

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
        public static List<PhotoList> imagesavelist = new List<PhotoList>();
        public static List<PhotoList1> imagesavelist1 = new List<PhotoList1>();
        private void btnBrowse_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;

                MooringRopeInspectionClass obj = clickedButton.DataContext as MooringRopeInspectionClass;

              

                if (clickedButton.Content == "Delete Image")
                {
                    SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image1=null,Photo1=null where ID= " + obj.Id + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    imagesavelist1.ForEach(x => {
                        if (x.Id == obj.Id && x.RopeId == obj.RopeId)
                        {
                            x.photo2 = null;
                            x.imagename2 = null;


                        }
                    });

                    clickedButton.Content = "Browse";

                    MessageBox.Show("Image deleted successfully", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MooringRopeInspectionClass cls = new MooringRopeInspectionClass();

                            // string ServerName = StaticHelper.ServerName;
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + DateTime.Now.ToString("dd -MMM-yyyy") + "";


                            string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\";

                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }

                System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                            fldlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                            fldlg.ShowDialog();
                {

                    try
                    {
                        if (imagesavelist1 != null && imagesavelist1.Count != 0)
                        {
                            var itemToRemove = imagesavelist1.Single(r => r.RopeId == obj.RopeId);
                            imagesavelist1.Remove(itemToRemove);

                        }
                    }
                    catch { }

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
                        MessageBox.Show("Image Size can not exceed 200Kb !", "Mooring Tail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        byte[] ss = System.IO.File.ReadAllBytes(imageName);
                        MooringTailInspectionViewModel._mooringInspact.Photo2 = ss;

                        MooringTailInspectionViewModel._mooringInspact.Image2 = strName;


                        PhotoList1 myphoto = new PhotoList1()
                        {
                            Id = obj.Id,
                            RopeId = obj.RopeId,
                            WinchId = obj.WinchId,
                            photo2 = null,
                            imagename2 = fileName

                        };
                        imagesavelist1.Add(myphoto);

                        //string fileName = System.IO.Path.GetFileName(strName);
                        //mypath = mypath + "\\" + fileName;

                        MooringTailInspectionViewModel.loadUserAccess.ToList().ForEach(x =>
                        {
                            if (x.Id == myphoto.Id)
                            {
                                x.Photo12 = "Hidden";
                                x.Showbutton2 = "Visible";
                                x.Photo2 = null;
                                x.Image2 = fileName;
                                x.ButtonContent2 = "Delete Image";

                                //StaticHelper. = mw.RopeId;
                                //photoimglist1 = null;
                            };
                        });



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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();


            //saveFileDialog1.Filter = "bak Files | *.bak";
            //saveFileDialog1.DefaultExt = "bak";
            //saveFileDialog1.InitialDirectory = @"C:\";
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.Title = "Save File";
            //saveFileDialog1.FileName = "shipment.bak";

            sfd.FileName = "MOORING ROPES INSPECTION GUIDE.pdf";
            sfd.DefaultExt = ".pdf";
            //sfd.Filter = "Backup Files|*.bak";
            sfd.Title = "Save File";
            // var ss = @"C:\Program Files (x86)\DigiMoorX7\MOORING ROPES INSPECTION GUIDE.pdf";
            var abc = System.IO.Directory.GetCurrentDirectory();
            var ss = abc + @"\MOORING ROPES INSPECTION GUIDE.pdf";
            // var kk = @"C:\Users\49webstreet\Downloads\Microsoft.SkypeApp_kzf8qxf38zg5c!App\All\MOORING ROPES INSPECTION GUIDE.pdf";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);

                }
                else
                {

                    File.Copy(ss, sfd.FileName);
                }
                System.Windows.MessageBox.Show("Download successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;

                int check = Convert.ToInt32(txtbx.Text);

                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Lenngth of Abrasion' cannot be greater than 2 digits & 2 decimal places", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
               
            }
            catch (Exception ex) { }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Distance from Outboard eye' cannot be greater than 2 digits & 2 decimal places", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Cut Yarn counted' cannot be greater than 2 digits & 2 decimal places", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Length of Glazing' cannot be greater than 2 digits & 2 decimal places", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }
        }
    }
}
