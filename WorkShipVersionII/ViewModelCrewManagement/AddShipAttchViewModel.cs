using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddShipAttchViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

        public AddShipAttchViewModel()
        {
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;
            _BrowseCommand = new RelayCommand(BrowseMethod);
            saveCommand = new RelayCommand(SaveAttachment);
            cancelCommand = new RelayCommand(CancelDepartment);
        }

        public AddShipAttchViewModel(int menuid)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            Mymenuid = menuid;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;
            _BrowseCommand = new RelayCommand(BrowseMethod);
            saveCommand = new RelayCommand(SaveAttachment);
            cancelCommand = new RelayCommand(CancelDepartment);
            //EditDepartment(edeps);
        }

        private static int Mymenuid { get; set; }

        private void BrowseMethod()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".docx";
            dlg.Filter = "Image files (*.png;*.jpeg;*.jpg;*.tiff)|*.png;*.jpeg;*.jpg;*.tiff|PDF files (*.pdf)|*.pdf;|Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm;|Word Files (*.doc;*.docx)|*.doc;*.docx";
            //"Excel Files|*.xls;*.xlsx;*.xlsm;*.pdf;*.jpg;*.png;*.jpeg";

            // Display OpenFileDialog by calling ShowDialog method
            //Nullable<bool> result1 = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                AddDepartment.AttachmentPath = dlg.FileName;
                RaisePropertyChanged("AddDepartment");
                
            }
        }


        private string departmentMessage;
        public string DepartmentMessage
        {
            get { return departmentMessage; }
            set { departmentMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("DepartmentMessage")); }
        }

        private ShipSpecificAttachment _AddDepartment = new ShipSpecificAttachment();
        public ShipSpecificAttachment AddDepartment
        {
            get
            {
                DepartmentMessage = string.Empty;
                RaisePropertyChanged("DepartmentMessage");
                return _AddDepartment;
            }
            set
            {
                _AddDepartment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddDepartment"));
            }
        }

        private ICommand _BrowseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                return _BrowseCommand;
            }
        }
        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }


        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private void SaveAttachment()
        {
            try
            {

                if(!string.IsNullOrEmpty(AddDepartment.AttachmentName))
                {
                    AddDepartment.AttachmentName = AddDepartment.AttachmentName;
                    AddDepartment.CreateBy = "Admin";
                    AddDepartment.CreatedDate = DateTime.Now;
                    AddDepartment.MId = Mymenuid;
                    AddDepartment.ShipId = "Attachment";
                    AddDepartment.ModifiedBy = "Admin";
                    AddDepartment.ModifiedDate = DateTime.Now;
                    AddDepartment.Type = "Ship";

                                   string ServerName = StaticHelper.ServerName;
                                   string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\";

                                   //string mypath = @"C:\DigiMoorDB_Backup\Attachment";
                    //string subdir = @"C:\Temp\Mahesh";
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(mypath))
                    {
                        Directory.CreateDirectory(mypath);
                    }


                    if (!string.IsNullOrEmpty(AddDepartment.AttachmentPath))
                    {
                        if (File.Exists(AddDepartment.AttachmentPath))
                        {
                            System.Windows.Forms.OpenFileDialog fldlg = new System.Windows.Forms.OpenFileDialog();
                            fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                            string fileExtention = Path.GetExtension(AddDepartment.AttachmentPath);//Path.GetFileName(AddDepartment.AttachmentPath);

                            string[] ExtList = { ".xls", ".xlsx", ".xlsm", ".pdf", ".jpg", ".png", ".jpeg", ".tiff", ".doc",".docx" };
                            if (ExtList.Contains(fileExtention.ToLower()))
                            {
                                string imageName = fldlg.FileName;
                                string fileName = Path.GetFileName(AddDepartment.AttachmentPath);

                                Random randon = new Random();
                                int num = randon.Next(10000);

                                string fileExtention1 = System.IO.Path.GetExtension(imageName);
                                //string fileName = System.IO.Path.GetFileName(fldlg.FileName);
                                var withoutextnsn = System.IO.Path.GetFileNameWithoutExtension(fileName) + num;

                                fileName = withoutextnsn + fileExtention;



                                mypath = mypath + "\\" + fileName;
                                //File.Delete(sfd.FileName);
                                var ss = AddDepartment.AttachmentPath;
                                                        //AddDepartment.AttachmentPath = mypath;

                                                        AddDepartment.AttachmentPath = fileName;


                                                        sc.ShipSpecificAttachments.Add(AddDepartment);
                                sc.SaveChanges();

                                try
                                {
                                    File.Copy(ss, mypath);
                                }
                                catch (Exception ex) { }

                                RefreshShipAttach();

                                //StaticHelper.Editing = false;
                                MessageBox.Show("Record saved successfully ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AddDepartment = new ShipSpecificAttachment();
                                RaisePropertyChanged("AddDepartment");
                                CancelDepartment();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Attachment file.", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        //else
                        //{
                        //    File.Copy(obj.AttachmentPath, sfd.FileName);
                        //}


                        
                    }
                    else
                    {
                        MessageBox.Show("Please Browse the file for Attachment ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the file name ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void RefreshShipAttach()
        {
            //LoadAttachmentDetail.Clear();
           ShipAttachViewModel.loadCrewDetail.Clear();
            var data = sc.ShipSpecificAttachments.Where(x => x.MId == Mymenuid).ToList();
            sc.ObservableCollectionList(ShipAttachViewModel.loadCrewDetail, data);
            
            //foreach (var item in data)
            //{

            //}
        }

        private void UpdateDepartment(ShipSpecificAttachment deps)
        {
            try
            {
                //deps.DeptName = deps.DeptName != null ? deps.DeptName.Trim() : deps.DeptName;
                //if (!string.IsNullOrEmpty(deps.DeptName))
                //{

                //    var findrank = sc.Departments.Where(x => x.did == deps.did).FirstOrDefault();

                //    if (findrank != null)
                //    {
                //        deps.DeptName = textinfo.ToTitleCase(deps.DeptName.ToLower());



                //        var local = sc.Set<DepartmentClass>()
                //         .Local
                //         .FirstOrDefault(f => f.did == deps.did);
                //        if (local != null)
                //        {
                //            sc.Entry(local).State = EntityState.Detached;
                //        }

                //        var UpdatedLocation = new DepartmentClass()
                //        {

                //            did = deps.did,
                //            DeptName = deps.DeptName
                //        };

                //        sc.Entry(UpdatedLocation).State = EntityState.Modified;
                //        sc.SaveChanges();


                //        //Update into User's Table
                //        var user = sc.CrewDetails.Where(x => x.did.Equals(UpdatedLocation.did)).ToList();
                //        var depat = user.Where(x => x.did.Equals(UpdatedLocation.did)).FirstOrDefault().department;
                //        user.ForEach(a =>
                //        {
                //            a.department = UpdatedLocation.DeptName;
                //        });

                //        sc.SaveChanges();

                //        //Update into WorkHours's Table
                //        var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
                //        some.ForEach(a =>
                //        {
                //            a.Department = UpdatedLocation.DeptName;
                //        });

                //        sc.SaveChanges();
                //        StaticHelper.Editing = false;
                //        MessageBox.Show("Record updated successfully", "Update Department", MessageBoxButton.OK, MessageBoxImage.Information);


                //        CancelDepartment();

                //    }

                //}
                //else
                //{

                //    DepartmentMessage = "Please Enter the Department Name";
                //    RaisePropertyChanged("DepartmentMessage");
                //}

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


 

        private void CancelDepartment()
        {
            //var lostdata = new ObservableCollection<DepartmentClass>(sc.Departments.ToList());
            //DepartmentViewModel cc = new DepartmentViewModel(lostdata);

            new ShipSpecificAttachment();
           
            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
