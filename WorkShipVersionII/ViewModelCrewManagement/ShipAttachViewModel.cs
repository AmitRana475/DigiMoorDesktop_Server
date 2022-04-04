using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
//using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class ShipAttachViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private static int menuid = 0;
        public ShipAttachViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            
            addCommand = new RelayCommand(AddShipAttach);
            editCommand = new RelayCommand<ShipSpecificAttachment>(Downloadfile);
            deleteCommand = new RelayCommand<ShipSpecificAttachment>(DeleteAttachment);
        }


        public ShipAttachViewModel(int mid)
        {

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            menuid = mid;
            
            addCommand = new RelayCommand(AddShipAttach);
            editCommand = new RelayCommand<ShipSpecificAttachment>(Downloadfile);
            deleteCommand = new RelayCommand<ShipSpecificAttachment>(DeleteAttachment);

            var data = sc.ShipSpecificAttachments.Where(x => x.MId == mid).ToList();

            LoadAttachmentDetail.Clear();
            sc.ObservableCollectionList(LoadAttachmentDetail, data);

        }

      
        private void AddShipAttach()
        {
            AddShipAttchViewModel vm = new AddShipAttchViewModel(menuid);
            ChildWindowManager.Instance.ShowChildWindow(new AddShipAttchView() { DataContext = vm });
        }
        private void Downloadfile(ShipSpecificAttachment obj)
        {
            try
            {
                            string ServerName = StaticHelper.ServerName;
                            string path = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + obj.AttachmentPath + "";

                            if (!string.IsNullOrEmpty(path))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    //sfd.CheckFileExists = true;
                    //sfd.CheckPathExists = true;
                    string extion = Path.GetExtension(path);
                    sfd.FileName = obj.AttachmentName + extion;
                    sfd.DefaultExt = extion;
                    sfd.Filter = "All files (*.*)|*.*";//"Image files (*.png;*.jpeg;*.jpg;*.tiff)|*.png;*.jpeg;*.jpg;*.tiff|PDF files (*.pdf)|*.pdf;|Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm;|Word Files (*.doc;*.docx)|*.doc;*.docx";
                    sfd.Title = "Save File";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            File.Delete(sfd.FileName);
                            File.Copy(path, sfd.FileName);
                        }
                        else
                        {
                            File.Copy(path, sfd.FileName);
                        }
                        MessageBox.Show("Attachment is saved successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Source file not found!");
                }


            }
            catch 
            {

                MessageBox.Show("File not found!");
                
            }
        }
        private void DeleteAttachment(ShipSpecificAttachment obj)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var finddata = sc.ShipSpecificAttachments.Where(x => x.Id == obj.Id).FirstOrDefault();
                    if (finddata != null)
                    {
                        if (finddata.Type == "Ship")
                        {
                                                 string ServerName = StaticHelper.ServerName;
                                                 string path = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + obj.AttachmentPath + "";
                                                 if (File.Exists(path))
                            {
                                File.Delete(path);

                            }

                            sc.Entry(finddata).State = EntityState.Deleted;
                            sc.SaveChanges();

                            LoadAttachmentDetail.Clear();

                            var data = sc.ShipSpecificAttachments.Where(x => x.MId == menuid).ToList();
                            sc.ObservableCollectionList(LoadAttachmentDetail, data);
                            MessageBox.Show("Attachment is Deleted successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                                          {
                                                 MessageBox.Show("This attachment is not allowed to be deleted, please contact your office for amendment!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand; }
            set { addCommand = value; }
        }

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        public static ObservableCollection<ShipSpecificAttachment> loadCrewDetail = new ObservableCollection<ShipSpecificAttachment>();
        public ObservableCollection<ShipSpecificAttachment> LoadAttachmentDetail
        {
            get
            {
                return loadCrewDetail;
            }
            set
            {
                loadCrewDetail = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadAttachmentDetail"));
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
