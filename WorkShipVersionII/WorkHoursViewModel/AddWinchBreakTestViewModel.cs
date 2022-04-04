using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class AddWinchBreakTestViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

              public AddWinchBreakTestViewModel()
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
              private static int Mymenuid { get; set; }
              public AddWinchBreakTestViewModel(int menuid)
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
              }

              private void BrowseMethod()
              {
                     OpenFileDialog dlg = new OpenFileDialog();

                     // Set filter for file extension and default file extension
                     dlg.DefaultExt = ".docx";
                     dlg.Filter = "Image files (*.png;*.jpeg;*.jpg;*.tiff)|*.png;*.jpeg;*.jpg;*.tiff|PDF files (*.pdf)|*.pdf;|Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm;|Word Files (*.doc;*.docx)|*.doc;*.docx;|All Videos Files |*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                           " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm";

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

              private WinchBrakeTestAttachmentClass _AddDepartment = new WinchBrakeTestAttachmentClass();
              public WinchBrakeTestAttachmentClass AddDepartment
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

                            if (string.IsNullOrEmpty(AddDepartment.AttachmentName))
                            {
                                   MessageBox.Show("Please enter the file name ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                            else if (AddDepartment.CreatedDate == null )
                            {
                                   MessageBox.Show("Please Select Date ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                            else
                            {
                                 //  AddDepartment.AttachmentName = AddDepartment.AttachmentName;

                                  // AddDepartment.CreatedDate = DateTime.Now;

                                   string ServerName = StaticHelper.ServerName;
                                   string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment";



                                   // string mypath = @"C:\DigiMoorDB_Backup\Attachment";
                                   //string subdir = @"C:\Temp\Mahesh";
                                   // If directory does not exist, create it. 
                                   if (!Directory.Exists(mypath))
                                   {
                                          Directory.CreateDirectory(mypath);
                                   }

                                   // Guid g = Guid.NewGuid();
                                   Random randon = new Random();
                                   int num = randon.Next(10000);


                                   if (!string.IsNullOrEmpty(AddDepartment.AttachmentPath))
                                   {
                                          if (File.Exists(AddDepartment.AttachmentPath))
                                          {
                                                 string fileExtention = Path.GetExtension(AddDepartment.AttachmentPath);//Path.GetFileName(AddDepartment.AttachmentPath);

                                                 string[] ExtList = { ".xls", ".xlsx", ".wmv", ".xlsm", ".pdf", ".mp4", ".jpg", ".png", ".jpeg", ".tiff", ".doc", ".docx" };
                                                 if (ExtList.Contains(fileExtention.ToLower()))
                                                 {

                                                        //var count = sc.TrainingAttachment.Count();
                                                        //if (count <= 9)
                                                        //{


                                                        string fileName = Path.GetFileName(AddDepartment.AttachmentPath);
                                                        var withoutextnsn = Path.GetFileNameWithoutExtension(fileName) + num;

                                                        fileName = withoutextnsn + fileExtention;

                                                        mypath = mypath + "\\" + fileName;
                                                        //File.Delete(sfd.FileName);

                                                        var ss = AddDepartment.AttachmentPath;

                                                        //AddDepartment.AttachmentPath = mypath;
                                                        AddDepartment.AttachmentPath = fileName;


                                                        sc.WinchBrakeTestAttachments.Add(AddDepartment);
                                                        sc.SaveChanges();

                                                        
                                                        try
                                                        {
                                                               File.Copy(ss, mypath);
                                                        }
                                                        catch (Exception ex) { }

                                                        //RefreshShipAttach();

                                                        //StaticHelper.Editing = false;
                                                        MessageBox.Show("Record saved successfully ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        CancelDepartment();

                                                       WinchBrakeTestRecordViewModel.StaticExample();

                                                        //}else
                                                        //{
                                                        //    MessageBox.Show("Attachment could not more than 9", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        //}
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


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void RefreshShipAttach()
              {

                     //TrainingAttachmentViewModell.loadCrewDetail.Clear();
                     //var data = sc.TrainingAttachment.ToList();
                     //sc.ObservableCollectionList(TrainingAttachmentViewModell.loadCrewDetail, data);


              }

              private void CancelDepartment()
              {
                     AddDepartment = new WinchBrakeTestAttachmentClass();
                     RaisePropertyChanged("AddDepartment");
                     ChildWindowManager.Instance.CloseChildWindow();
              }

                           

              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }
       }
}
