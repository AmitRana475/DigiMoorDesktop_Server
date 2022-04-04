using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Controls.Primitives;
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
    public partial class MooringRopeInspectionView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public MooringRopeInspectionView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            //MooringRopeInspectionGrid.FrozenColumnCount = 3;
        }

       

        private void cboExternal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
              
                //MooringRopeInspectionGrid.Columns[0].IsFrozen = true;
                if (sender is ComboBox)
                {
                    ComboBox comBox = (ComboBox)e.OriginalSource;
                    // get DataGridRow which checkBox exits.
                    MooringRopeInspectionView rowData = comBox.DataContext as MooringRopeInspectionView;
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

                    if (secondvalue == 0)
                    {
                        txts.Text = "";
                    }

                    //MooringRopeInspectionViewModel._mooringInspact.AverageRating_A = Convert.ToInt32(finalvalue);
                }
            }
            catch (Exception ex) { }
        }

        private void cboInternal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                if (sender is ComboBox)
                {
                    ComboBox comBox = (ComboBox)e.OriginalSource;

                    
                    // get DataGridRow which checkBox exits.
                    MooringRopeInspectionView rowData = comBox.DataContext as MooringRopeInspectionView;
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

                    if (secondvalue == 0)
                    {
                        txts.Text = "";
                    }
                }
                // MooringRopeInspectionViewModel._mooringInspact.AverageRating_A = Convert.ToInt32(finalvalue);
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
                    MooringRopeInspectionView rowData = comBox.DataContext as MooringRopeInspectionView;
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

                    if (secondvalue == 0)
                    {
                        txts.Text = "";
                    }
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
                    MooringRopeInspectionView rowData = comBox.DataContext as MooringRopeInspectionView;
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

                    if (secondvalue == 0)
                    {
                        txts.Text = "";
                    }
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

        private int contenctchanged;

        public int Content
        {
            get { return contenctchanged; }
            set { contenctchanged = value; }
        }

        public DataTable dtClass =  new DataTable();

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {          
          
            try
            {
                            string ServerName = StaticHelper.ServerName;
                            Button clickedButton = sender as Button;
              
                MooringRopeInspectionClass obj = clickedButton.DataContext as MooringRopeInspectionClass;

              

                if(clickedButton.Content== "Delete Image")
                {
                    SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image1=null,Photo1=null where ID= " + obj.Id + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    // clickedButton.Content = "Browse";

                    imagesavelist.ForEach(x => {
                        if (x.Id == obj.Id && x.RopeId== obj.RopeId)
                        {
                            x.photo1 = null;
                            x.imagename1 = null;
                            if (File.Exists(x.Paht1))
                            {
                                File.Delete(x.Paht1);
                            }

                        }
                    });

                    
                   
                    clickedButton.Content = "Browse";

                                   //var imgname=sc.MooringRopeInspectionTbl

                                  

                                   string path = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";

                                  // var path = @"C:\DigiMoorDB_Backup\InspectionImages\";

                    //clickedButton.Visibility = Visibility.Hidden;

                    MessageBox.Show("Image deleted successfully", "Line Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                MooringRopeInspectionClass cls = new MooringRopeInspectionClass();

                            //string ServerName = StaticHelper.ServerName;
                            // string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + DateTime.Now.ToString("dd -MMM-yyyy") + "";

                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\" + DateTime.Now.ToString("dd-MMM-yyyy") + "";


                            //string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages";

                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }

                //var BtnCell =  (System.Windows.Forms.DataGridViewButtonCell)MooringRopeInspectionGrid.RowStyle[1].Cells[1];
                //BtnCell.Value = "New Button Value";

                System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                //fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif,*.PNG)|*.jpg;*.bmp;*.gif,*.PNG";

                Random randon = new Random();
                int num = randon.Next(10000);


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

                    string fileName = System.IO.Path.GetFileName(strName);
                   

                    string fileExtention = System.IO.Path.GetExtension(imageName);
                    //string fileName = System.IO.Path.GetFileName(fldlg.FileName);
                    var withoutextnsn = System.IO.Path.GetFileNameWithoutExtension(fileName) + num;

                    fileName = withoutextnsn + fileExtention;

                    mypath = mypath + "\\" + fileName;

                    //string imageName = imageName1;

                    var size = new FileInfo(fldlg.FileName).Length / 1024;

                    if (size > 200)
                    {
                        MessageBox.Show("Image Size can not exceed 200Kb !" , "Mooring Line Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {

                        // decimal size = Math.Round(((decimal)fldlg.FileName.Length / (decimal)1024), 2);

                       // imagesavelist.Clear();
                        byte[] ss = System.IO.File.ReadAllBytes(imageName);
                        MooringRopeInspectionViewModel._mooringInspact.Photo1 = ss;

                        MooringRopeInspectionViewModel._mooringInspact.Image1 = strName;


                        PhotoList myphoto = new PhotoList()
                        {
                            Id = obj.Id,
                            RopeId = obj.RopeId,
                            WinchId = obj.WinchId,
                            //photo1 = ss,
                            photo1 = null,
                            imagename1 = fileName,
                            Paht1 = mypath

                        };
                        imagesavelist.Add(myphoto);


                      //  var kkkk =MooringRopeInspectionViewModel.loadUserAccess.ToList();
                        MooringRopeInspectionViewModel.loadUserAccess.ToList().ForEach(x =>
                        {
                            if (x.Id == myphoto.Id)
                            {
                                x.Photo11 = "Hidden";
                                x.Showbutton1 = "Visible";
                                x.Photo1 = null;
                                x.Image1 = fileName;
                                x.ButtonContent1 = "Delete Image";
                               
                               // x.Paht2 = fldlg.FileName;


                                //StaticHelper. = mw.RopeId;
                                //photoimglist1 = null;
                            };
                        });

                        //LoadInspections.Clear();
                        //sc.ObservableCollectionList(LoadInspections, list);

                        //MooringRopeInspectionView.imagesavelist.ForEach(x => {
                        //    if (x.RopeId == obj.RopeId)
                        //    {
                        //        x.photo1 = ss;
                        //        x.imagename1 = fileName;
                        //    }
                        //});




                        //var path = System.IO.Path.Combine(mypath, System.IO.Path.GetFileName(strName));

                        //File.Delete(sfd.FileName);

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
                            string ServerName = StaticHelper.ServerName;
                            Button clickedButton = sender as Button;
                MooringRopeInspectionClass obj = clickedButton.DataContext as MooringRopeInspectionClass;            

                if (clickedButton.Content == "Delete Image")
                {
                    //SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image1=null,Photo1=null where ID= " + obj.Id + "", sc.con);
                    //DataTable dt = new DataTable();
                    //adp.Fill(dt);

                    SqlDataAdapter adp = new SqlDataAdapter("UpdateMooringRopeInspection", sc.con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@Id", obj.Id);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adp.Fill(dt);
                    // clickedButton.Content = "Browse";

                    //MooringRopeInspectionViewModel.loadUserAccess.ToList().ForEach(x =>
                    //{
                    //    if (x.Id == obj.Id)
                    //    {

                    //        x.ButtonContent2 = "Browse";
                    //        x.Photo2 = null;
                    //        x.Image2 = null;
                    //    };
                    //});

                    imagesavelist1.ForEach(x => {
                        if (x.Id == obj.Id && x.RopeId == obj.RopeId)
                        {
                            x.photo2 = null;
                            x.imagename2 = null;

                            if (File.Exists(x.Paht2))
                            {
                                File.Delete(x.Paht2);
                            }
                        }
                    });

                    clickedButton.Content = "Browse";
                    MessageBox.Show("Image deleted successfully", "Line Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                MooringRopeInspectionClass cls = new MooringRopeInspectionClass();


                            //string ServerName = StaticHelper.ServerName;
                            //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
                            // string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + DateTime.Now.ToString("dd -MMM-yyyy") + "";

                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\" + DateTime.Now.ToString("dd-MMM-yyyy") + "";

                            string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                            //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages";

                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }


                System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
               // fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
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
                        MessageBox.Show("Image Size can not exceed 200Kb !", "Mooring Line Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                       // imagesavelist1.Clear();

                        byte[] ss = System.IO.File.ReadAllBytes(imageName);
                        MooringRopeInspectionViewModel._mooringInspact.Photo2 = ss;

                        MooringRopeInspectionViewModel._mooringInspact.Image2 = strName;

                        PhotoList1 myphoto = new PhotoList1()
                        {
                            Id=obj.Id,
                            RopeId = obj.RopeId,
                            WinchId = obj.WinchId,
                            photo2 = null,
                            imagename2 = fileName,
                            Paht2=mypath

                        };
                        imagesavelist1.Add(myphoto);

                        //string fileName = System.IO.Path.GetFileName(strName);
                        //mypath = mypath + "\\" + fileName;


                        MooringRopeInspectionViewModel.loadUserAccess.ToList().ForEach(x =>
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

                        //clickedButton.Content = "Delete Image";
                    }
                }
                fldlg = null;
            }
            catch (Exception ex)
            {

            }
        }

        private void Edit1_Click(object sender, RoutedEventArgs e)
        {
            //DataRowView Grdrow = ((FrameworkElement)sender).DataContext as DataRowView;
            ////Fidn the DataGrid row index
            //int rowIndex = MooringRopeInspectionGrid.Items.IndexOf(Grdrow);
            ////Find the DataGridCell
            //DataGridCell cell = GetCell(rowIndex, 1); //Pass the row and column
            ////Find the "lblVehicleName" lable.
            //Label lblsource_address = GetVisualChild<Label>(cell); // pass the DataGridCell as a parameter to GetVisualChild
            //string _value = lblsource_address.ContentStringFormat;
        }
        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    MooringRopeInspectionGrid.ScrollIntoView(rowContainer, MooringRopeInspectionGrid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }
        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)MooringRopeInspectionGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                MooringRopeInspectionGrid.UpdateLayout();
                MooringRopeInspectionGrid.ScrollIntoView(MooringRopeInspectionGrid.Items[index]);
                row = (DataGridRow)MooringRopeInspectionGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>
                    (v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           System.Windows.Forms. SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();


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

        private void feesAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void chkDiscontinueb_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox comBox = (CheckBox)e.OriginalSource;
                // get DataGridRow which checkBox exits.
                MooringRopeInspectionView rowData = comBox.DataContext as MooringRopeInspectionView;
                DataGridRow dataGridRow = DataGridRow.GetRowContainingElement(comBox);

               // string text = ((CheckBox)comBox.).Content.ToString();

                DataGridCellInfo cells = MooringRopeInspectionGrid.SelectedCells[1];

                MooringRopeInspectionClass item = cells.Item as MooringRopeInspectionClass;
                object columnName = cells.Item.GetType().GetProperty("IsCheckedV").GetValue(item, null);

                MooringRopeInspectionViewModel._mooringInspact.IsEnable = "True";

                MooringRopeInspectionViewModel sdsd = new MooringRopeInspectionViewModel();

                //if (item.IsCheckedV == false)
                //{
                //    sdsd.GetMooringInspection();
                //}
                //else
                //{
                //    sdsd.GetMooringInspection2();
                //}
                //RaisePropertyChanged("MooringInspect");
                // RaiseEvent("AddMooringWinchRope");
                //OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
            }
        }

        private void chkDiscontinueb_Checked(object sender, RoutedEventArgs e)
        {
            MooringRopeInspectionGrid.Columns[0].IsReadOnly = true; // disable the entire column of all rows

            // to disable only the corresponding cell of the desired column
            CheckBox chk = (CheckBox)sender;
            //Ellipse objTextbox = (Ellipse)sender;
            DataGridRow row = (DataGridRow)MooringRopeInspectionGrid.ItemContainerGenerator.ContainerFromItem(chk.DataContext);
            DataGridCell p = (DataGridCell)((TextBox)MooringRopeInspectionGrid.Columns[5].GetCellContent(row)).Parent;
            p.IsEnabled = false;
        }

        private void chkDiscontinueb_Unchecked(object sender, RoutedEventArgs e)
        {
            MooringRopeInspectionGrid.Columns[0].IsReadOnly = false;

            CheckBox chk = (CheckBox)sender;
            DataGridRow row = (DataGridRow)MooringRopeInspectionGrid.ItemContainerGenerator.ContainerFromItem(chk.DataContext);
            DataGridCell p = (DataGridCell)((TextBlock)MooringRopeInspectionGrid.Columns[3].GetCellContent(row)).Parent;
            p.IsEnabled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;

                int check = Convert.ToInt32(txtbx.Text);

                if (check > 99)
                {
                   // Value in 'Length of Abrasion A' cannot be greater than 2 digits & 2 decimal places
                    MessageBox.Show("Value in 'Length of Abrasion A' cannot be greater than 2 digits & 2 decimal places", " Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                   
                }
                //if (check == false)
                //{
                //    DataGridRow dataGridRow = DataGridRow.GetRowContainingElement(txtbx);
                //    TextBlock txts = MooringRopeInspectionGrid.SelectedCells[9].Column.GetCellContent(dataGridRow.Item) as TextBlock;

                //    txts.Text = "";
                //}
            }
            catch (Exception ex) { }
            //try
            //{
            //    
            //}
            //catch { }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    MessageBox.Show("Value in 'Distance from Outboard eye A' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    // Value in 'Cut Yarn counted A' cannot be greater than 2 digits & 2 decimal places
                    MessageBox.Show("Value in 'Cut Yarn counted A' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    
                    MessageBox.Show("Value in 'Length of Glazing A' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }
        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;

                int check = Convert.ToInt32(txtbx.Text);

                if (check > 99)
                {
                   
                    MessageBox.Show("Value in 'Lenngth of Abrasion B' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
               
            }
            catch (Exception ex) { }
        }

        private void TextBox_TextChanged_5(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Distance from Outboard eye B' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }
        }

        private void TextBox_TextChanged_6(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Cut Yarn counted B' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }

        }

        private void TextBox_TextChanged_7(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;
                int check = Convert.ToInt32(txtbx.Text);
                if (check > 99)
                {
                    
                    MessageBox.Show("Value in 'Length of Glazing B' cannot be greater than 2 digits & 2 decimal places", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { }
        }

      
    }

    public class PhotoList
    {
        public int Id { get; set; }
        public int RopeId { get; set; }
        public int WinchId { get; set; }
        public byte[] photo1 { get; set; }
        public string imagename1 { get; set; }
        public string Paht1 { get; set; }
        
       

    }

    public class PhotoList1
    {
        public int Id { get; set; }
        public int RopeId { get; set; }
        public int WinchId { get; set; }
       
        public byte[] photo2 { get; set; }
        public string imagename2 { get; set; }
        public string Paht2 { get; set; }
    }
}
