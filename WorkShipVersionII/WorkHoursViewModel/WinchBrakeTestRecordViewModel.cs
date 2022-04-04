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
using WorkShipVersionII.Views;
using System.Windows.Data;
using WorkShipVersionII.Commands;
using System.Data.SqlClient;
using System.Data;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class WinchBrakeTestRecordViewModel: ViewModelBase
       {
              public ICommand HelpCommand { get; private set; }
              private readonly ShipmentContaxt sc;
              private static int itemPerPage = 10;
              private int itemcount;
              private static int menuid = 0;
              public WinchBrakeTestRecordViewModel()
              {
                     var ss = @"4.11_WINCH_BRAKE_TEST_RECORDS.htm";
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     GetTrainingAttachment();


                     addCommand = new RelayCommand(AddShipAttach);
                     editCommand = new RelayCommand<WinchBrakeTestAttachmentClass>(Downloadfile);
                     deleteCommand = new RelayCommand<WinchBrakeTestAttachmentClass>(DeleteAttachment);
            //HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(ss));

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            NextCommand = new NextPageCommandWinchBrakeTest(this);
            PreviousCommand = new PreviousPageCommandWinchBrakeTest(this);
            FirstCommand = new FirstPageComandWinchBrakeTest(this);
            LastCommand = new LastPageCommandWinchBrakeTest(this);
        }

        public void GetTrainingAttachment()
              {
                     try
                     {
                            int rowid = 1;
                            LoadAttachmentDetail.Clear();
                            //SqlCommand cmd = new SqlCommand("ResidualList", sc.con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter("select * from WinchBrakeTestAttachment order by CreatedDate desc", sc.con);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadAttachmentDetail.Add(new WinchBrakeTestAttachmentClass()
                                   {
                                          RowId = rowid++,
                                          Id = (int)row["Id"],
                                          AttachmentName = (string)row["AttachmentName"],
                                          AttachmentPath = (string)row["AttachmentPath"],

                                          CreatedDate = (Convert.ToDateTime(row["CreatedDate"])),

                                   });
                            }
                            // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                            RaisePropertyChanged("LoadUserAccess");
                            ViewList = new CollectionViewSource
                            {
                                   Source = LoadAttachmentDetail
                            };
                            ViewList.Filter += new FilterEventHandler(View_Filter);

                            itemcount = LoadAttachmentDetail.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            ViewList.View.Refresh();

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }

              public static void StaticExample()
              {

                     WinchBrakeTestRecordViewModel example = new WinchBrakeTestRecordViewModel();
                     example.GetTrainingAttachment();
              }

              public WinchBrakeTestRecordViewModel(int mid)
              {
                     var ss = @"7.1_TYPES_OF_FILES_ALLOWED_TO_BE_UPLOADED_.htm";
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     menuid = mid;

                     addCommand = new RelayCommand(AddShipAttach);
                     editCommand = new RelayCommand<WinchBrakeTestAttachmentClass>(Downloadfile);
                     deleteCommand = new RelayCommand<WinchBrakeTestAttachmentClass>(DeleteAttachment);
                     // HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     // HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(ss));

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     var data = sc.WinchBrakeTestAttachments.ToList();

                     LoadAttachmentDetail.Clear();
                    sc.ObservableCollectionList(LoadAttachmentDetail, data);

              }
              private void AddShipAttach()
              {
                     AddWinchBreakTestViewModel vm = new AddWinchBreakTestViewModel(menuid);
                     ChildWindowManager.Instance.ShowChildWindow(new AddWinchBrakeTestRecord() { DataContext = vm });

                    
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
              #region PaginationWork

              public ICommand PreviousCommand { get; private set; }
              public ICommand NextCommand { get; private set; }
              public ICommand FirstCommand { get; private set; }
              public ICommand LastCommand { get; private set; }

              private static int _currentPageIndex;
              public int CurrentPageIndex
              {
                     get
                     {
                            CurrentPage = _currentPageIndex;
                            return _currentPageIndex;
                     }
                     set
                     {
                            _currentPageIndex = value;
                            RaisePropertyChanged("CurrentPageIndex");
                     }
              }




              private static int _CurrentPage;
              public int CurrentPage
              {
                     get
                     {
                            if (_totalPages > 0)
                                   return _currentPageIndex + 1;
                            return _CurrentPage;
                     }
                     set
                     {
                            _CurrentPage = value;
                            RaisePropertyChanged("CurrentPage");
                     }
              }
              private static int _totalPages;
              public int TotalPages
              {
                     get { return _totalPages; }
                     private set
                     {
                            _totalPages = value;
                            RaisePropertyChanged("TotalPages");
                     }
              }

              public void ShowNextPage()
              {
                     try
                     {
                            CurrentPageIndex++;
                            ViewList.View.Refresh();

                            //LoadUserAccess
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              public void ShowPreviousPage()
              {
                     try
                     {
                            CurrentPageIndex--;
                            ViewList.View.Refresh();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              public void ShowFirstPage()
              {
                     try
                     {
                            CurrentPageIndex = 0;
                            ViewList.View.Refresh();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              public void ShowLastPage()
              {
                     try
                     {
                            CurrentPageIndex = TotalPages - 1;
                            ViewList.View.Refresh();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }



              private void CalculateTotalPages()
              {
                     try
                     {
                            if (itemcount % itemPerPage == 0)
                            {
                                   TotalPages = (itemcount / itemPerPage);
                            }
                            else
                            {
                                   TotalPages = (itemcount / itemPerPage) + 1;
                            }
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void View_Filter(object sender, FilterEventArgs e)
              {
                     try
                     {
                            int index = ((WinchBrakeTestAttachmentClass)e.Item).RowId - 1;
                            if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                            {
                                   e.Accepted = true;
                            }
                            else
                            {
                                   e.Accepted = false;
                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              #endregion
              private void Downloadfile(WinchBrakeTestAttachmentClass obj)
              {
                     try
                     {

                            StaticHelper.ViewId = obj.Id;

                            string ServerName = StaticHelper.ServerName;
                            string path = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + obj.AttachmentPath + "";

                            System.Diagnostics.Process.Start(path);
                            // ChildWindowManager.Instance.ShowChildWindow(new ViewTrainingAttachment());


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void DeleteAttachment(WinchBrakeTestAttachmentClass obj)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                   var finddata = sc.WinchBrakeTestAttachments.Where(x => x.Id == obj.Id).FirstOrDefault();
                                   if (finddata != null)
                                   {


                                          if (File.Exists(finddata.AttachmentPath))
                                          {
                                                 File.Delete(finddata.AttachmentPath);

                                          }

                                          sc.Entry(finddata).State = EntityState.Deleted;
                                          sc.SaveChanges();

                                          
                                          GetTrainingAttachment();
                                          MessageBox.Show("Attachment is Deleted successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                   }
                            }
                     }
                     catch (Exception)
                     {

                            throw;
                     }
              }


              //public static CollectionViewSource ViewList { get; set; }
              private CollectionViewSource _ViewList = new CollectionViewSource();
              public CollectionViewSource ViewList
              {
                     get { return _ViewList; }
                     set
                     {
                            _ViewList = value;
                            RaisePropertyChanged("ViewList");
                     }
              }
              public static ObservableCollection<WinchBrakeTestAttachmentClass> loadCrewDetail = new ObservableCollection<WinchBrakeTestAttachmentClass>();

              // public static ObservableCollection<WinchBrakeTestAttachmentClass> loadCrewDetail = new ObservableCollection<WinchBrakeTestAttachmentClass>();
              public ObservableCollection<WinchBrakeTestAttachmentClass> LoadAttachmentDetail
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
