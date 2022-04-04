using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class MooringWinchViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public MooringWinchViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            viewCommand = new RelayCommand<MooringWinchClass>(ViewMooringWinchDetail);
            editCommand = new RelayCommand<MooringWinchClass>(EditMooringWinch);
            deleteCommand = new RelayCommand<MooringWinchClass>(DeleteMooringWinch);

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetMooringWinchList();
           // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

        }

        public MooringWinchViewModel(ObservableCollection<MooringWinchClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //LoadUserAccess.Clear();
            GetMooringWinchList();

        }

        private void ViewMooringWinchDetail(MooringWinchClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                ChildWindowManager.Instance.ShowChildWindow(new ViewMooringWinch());


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }
        private static string searchCrew1;
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

        private static ObservableCollection<MooringWinchClass> loadMooringWinchList = new ObservableCollection<MooringWinchClass>();

        public ObservableCollection<MooringWinchClass> LoadMooringWinchList
        {
            get
            {
                return loadMooringWinchList;
            }
            set
            {
                loadMooringWinchList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));

            }
        }


        public static ObservableCollection<MooringWinchClass> loadUserAccess = new ObservableCollection<MooringWinchClass>();
        public ObservableCollection<MooringWinchClass> LoadUserAccess
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
            }
        }

        //private ObservableCollection<MooringWinchClass> GetMooringWinchList()
        //{

        //    try
        //    {
        //        ObservableCollection<MooringWinchClass> moringlist = new ObservableCollection<MooringWinchClass>();

        //        var data = sc.MooringWinch.ToList();
        //        foreach (var item in data)
        //        {
        //            moringlist.Add(new MooringWinchClass()
        //            {
        //                Id = item.Id,
        //                AssignedNumber = item.AssignedNumber,
        //                Location = item.Location
        //            });


        //            }
        //        return moringlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        return null;
        //    }

        //}

        public void GetMooringWinchList()
        {
            try
            {
                LoadUserAccess.Clear();
              
               // var data = sc.MooringWinch.Where(x=> x.IsActive==true).ToList().OrderBy(x=> x.SortingOrder);

                SqlDataAdapter adp = new SqlDataAdapter("select * from mooringwinchdetail where isactive=1 order by SortingOrder asc", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    LoadUserAccess.Add(new MooringWinchClass()
                    {
                        Id = (int)row["Id"],
                        AssignedNumber = (string)row["AssignedNumber"],
                        Location = (string)row["Location"],
                        Lead = (string)row["Lead"],
                        MBL = (decimal)row["MBL"],
                        IsActive = (bool)row["IsActive"],
                        CreatedBy = (string)row["CreatedBy"],
                        CreatedDate = ((DateTime)row["CreatedDate"]),

                        SortingOrder = (int)row["SortingOrder"],

                      
                        //OutofServiceDate1 = ((DateTime)row["OutofServiceDate"]).ToString("yyyy-MM-dd"),
                        // OutofServiceDate = ((DateTime)row["OutofServiceDate"]),
                        //ReasonOutofService = (row["ReasonOutofService"] == DBNull.Value) ? string.Empty : row["ReasonOutofService"].ToString(),

                       // UniqueId = (row["UniqueId"] == DBNull.Value) ? null : row["UniqueId"].ToString(),

                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));



                //foreach (var item in data)
                //{
                //    LoadUserAccess.Add(new MooringWinchClass()
                //    {
                //        Id = item.Id,
                //        AssignedNumber = item.AssignedNumber,
                //        Location = item.Location,
                //        Lead = item.Lead,
                //        MBL = item.MBL,
                //        IsActive = item.IsActive,
                //        CreatedBy = item.CreatedBy,
                //        CreatedDate = item.CreatedDate,
                //        SortingOrder=item.SortingOrder
                //    });
                //}
                //OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
               
            }

        }

        private void EditMooringWinch(MooringWinchClass mw)
        {
            try
            {
                AddMooringWinchDetailsViewModel vm = new AddMooringWinchDetailsViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchDetail() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteMooringWinch(MooringWinchClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MooringWinchClass findrank = sc.MooringWinch.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {

                        var checkDepartment = sc.MooringWinch.Where(x => x.Id.Equals(mw.Id)).FirstOrDefault();
                        if (checkDepartment != null)
                        {
                            //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                            //}
                            //else
                            //{

                            SqlDataAdapter adp1 = new SqlDataAdapter("select IsDelete from AssignRopeToWinch where WinchId=" + mw.Id + "", sc.con);
                            DataTable dt1 = new DataTable();
                            adp1.Fill(dt1);
                            if (dt1.Rows.Count == 0)
                            {
                                //SqlDataAdapter adp = new SqlDataAdapter("update MooringWinchDetail set  IsActive='False' where Id =" + mw.Id + " ", sc.con);
                                SqlDataAdapter adp = new SqlDataAdapter("DeleteMooringWinchDetail", sc.con);
                                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);


                                sc.SaveChanges();

                                MessageBox.Show("Record deleted successfully ", "Delete MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            else
                            {
                                bool isdelete = Convert.ToBoolean(dt1.Rows[0][0]);

                                if (isdelete == true)
                                {
                                    //SqlDataAdapter adp = new SqlDataAdapter("update MooringWinchDetail set  IsActive='False' where Id =" + mw.Id + " ", sc.con);
                                    SqlDataAdapter adp = new SqlDataAdapter("DeleteMooringWinchDetail", sc.con);
                                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                                    DataTable dt = new DataTable();
                                    adp.Fill(dt);


                                    sc.SaveChanges();

                                    MessageBox.Show("Record deleted successfully ", "Delete MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                                }
                                else
                                {
                                    MessageBox.Show("This winch id cannot be deleted as it is already assigned to an active or past record of line / rope tail ! ", "Delete MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }



                            // sc.Entry(findrank).State = EntityState.Deleted;

                            //.....Refresh DataGrid........


                            var lostdata = new ObservableCollection<MooringWinchClass>(sc.MooringWinch.ToList());
                            MooringWinchViewModel cc = new MooringWinchViewModel(lostdata);



                            //sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList);

                            //.....End Refresh DataGrid........
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record is not found ", "Delete Department ", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
