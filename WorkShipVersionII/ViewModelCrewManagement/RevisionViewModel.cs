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
using System.Windows.Data;
using WorkShipVersionII.Commands;
using System.Data;
using System.Data.SqlClient;

namespace WorkShipVersionII.ViewModelCrewManagement
{
   public class RevisionViewModel:ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private static int itemPerPage = 10;
        private int itemcount;
        private static int menuid = 0;
        public RevisionViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;

                //RaisePropertyChanged("LoadRevision");



                //ViewList = new CollectionViewSource
                //{
                //    Source = LoadRevision
                //};


                //ViewList.Filter += new FilterEventHandler(View_Filter);


                //itemcount = LoadRevision.Count(); //sc.Notifications.Count();
                //CalculateTotalPages();
                //ViewList.View.Refresh();

                //NextCommand = new NextPageCommandRevision(this);
                //PreviousCommand = new PreviousPageCommandRevision(this);
                //FirstCommand = new FirstPageCommandRevision(this);
                //LastCommand = new LastPageCommandRevision(this);
            }        
        }
        public RevisionViewModel(int mid)
        {

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            menuid = mid;

            GetRevisionList(mid);
            //SqlDataAdapter adp = new SqlDataAdapter("select *,RowId = ROW_NUMBER() OVER(ORDER BY (SELECT 1)) from Revision  where Mid=" + mid + "", sc.con);
            //DataTable dt = new DataTable();
            //adp.Fill(dt);





            // var data = sc.Revisions.Where(x => x.Mid == mid).ToList();
            //var data = sc.Revisions.ToList();
           // var data = dt;

            //LoadRevision.Clear();
            //sc.ObservableCollectionList(LoadRevision, data);

            //RaisePropertyChanged("LoadRevision");



            //ViewList = new CollectionViewSource
            //{
            //    Source = LoadRevision
            //};


            //ViewList.Filter += new FilterEventHandler(View_Filter);


            //itemcount = LoadRevision.Count(); //sc.Notifications.Count();
            //CalculateTotalPages();
            //ViewList.View.Refresh();

            NextCommand = new NextPageCommandRevision(this);
            PreviousCommand = new PreviousPageCommandRevision(this);
            FirstCommand = new FirstPageCommandRevision(this);
            LastCommand = new LastPageCommandRevision(this);

        }
        public void GetRevisionList(int mid)
        {
            try
            {
                LoadRevision.Clear();
                SqlCommand cmd = new SqlCommand("select *,RowId = ROW_NUMBER() OVER(ORDER BY (SELECT 1)) from Revision  where Mid=" + mid + " order by ReviseDate desc", sc.con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadRevision.Add(new RevisionClass()
                    {

                        Id = (int)row["Id"],

                        RPrefix = (row["RPrefix"] == DBNull.Value) ? null : row["RPrefix"].ToString() + "/"+ Convert.ToDecimal((row["RNumber"] == DBNull.Value) ? 0.00 : row["RNumber"]),
                        RNumber = Convert.ToDecimal((row["RNumber"] == DBNull.Value) ? 0.00 : row["RNumber"]),

                        //RPrefix = (row["RPrefix"] == DBNull.Value) ? null : row["RPrefix"].ToString(),
                        //RNumber = Convert.ToDecimal((row["RNumber"] == DBNull.Value) ? 0.00 : row["RNumber"]),

                        //RopeType = (string)row["RopeType"],
                        //RopeTypeId = (int)row["RopeTypeId"],
                        //ManufacturerId = (int)row["ManufacturerId"],
                        ReviseDate = (Convert.ToDateTime(row["ReviseDate"])),
                        ApproveDate = (Convert.ToDateTime(row["ApproveDate"])),
                        Mid = Convert.ToInt32(row["Mid"] == DBNull.Value ? null : row["Mid"]),
                        RowId = Convert.ToInt32(row["RowId"] == DBNull.Value ? null : row["RowId"]),

                        CreateBy = (row["CreateBy"] == DBNull.Value) ? string.Empty : row["CreateBy"].ToString(),
                        Status = (row["Status"] == DBNull.Value) ? string.Empty : row["Status"].ToString(),
                        //Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                    });
                }
                //OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                RaisePropertyChanged("LoadRevision");



                ViewList = new CollectionViewSource
                {
                    Source = LoadRevision
                };


                ViewList.Filter += new FilterEventHandler(View_Filter);


                itemcount = LoadRevision.Count(); //sc.Notifications.Count();
                CalculateTotalPages();
                ViewList.View.Refresh();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }

        //private void AddShipAttach()
        //{
        //    AddShipAttchViewModel vm = new AddShipAttchViewModel(menuid);
        //    ChildWindowManager.Instance.ShowChildWindow(new AddShipAttchView() { DataContext = vm });
        //}



        public static CollectionViewSource ViewList { get; set; }
        public static ObservableCollection<RevisionClass> loadrevision = new ObservableCollection<RevisionClass>();
        public ObservableCollection<RevisionClass> LoadRevision
        {
            get
            {
                return loadrevision;
            }
            set
            {
                loadrevision = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadRevision"));
            }
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
                //int index = ((MOperationBirthDetail)e.Item).OPId + 1;
                //if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                int index = ((RevisionClass)e.Item).RowId - 1;
                //int index = 5;
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

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}

