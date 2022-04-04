using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class TailSplicingViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _ExportTailSplicing;
        public ICommand HelpCommand { get; private set; }
        public TailSplicingViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                _ExportTailSplicing = new BackgroundWorker();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            viewCommand = new RelayCommand<RopeSplicingClass>(ViewropeSplicing);
            editCommand = new RelayCommand<RopeSplicingClass>(EditCrossShiftingWinch);
            deleteCommand = new RelayCommand<RopeSplicingClass>(DeleteCrossShiftingWinch);
            GetRopeSplicingList();

            this._ExportTailSplicingCommands = new RelayCommand(() => _ExportTailSplicing.RunWorkerAsync(), () => !_ExportTailSplicing.IsBusy);
            this._ExportTailSplicing.DoWork += new DoWorkEventHandler(ExportTailSplicingMethod);
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
        //GetRopeSplicingList();

    }
        //ExportExcel
        private ICommand _ExportTailSplicingCommands;
        public ICommand ExportTailSplicingCommands
        {
            get
            {

                return _ExportTailSplicingCommands;
            }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }
        private void ViewropeSplicing(RopeSplicingClass mw)
        {
            try
            {
                
                StaticHelper.ViewId = mw.Id;
                StaticHelper.RopeTailId = 1;
                ChildWindowManager.Instance.ShowChildWindow(new ViewRopeSplicing());

               
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public TailSplicingViewModel(ObservableCollection<RopeSplicingClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            LoadUserAccess.Clear();

            GetRopeSplicingList();

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

        private static ObservableCollection<RopeSplicingClass> loadRopeSplicingList = new ObservableCollection<RopeSplicingClass>();

        public ObservableCollection<RopeSplicingClass> LoadRopeSplicingList
        {
            get
            {
                return loadRopeSplicingList;
            }
            set
            {
                loadRopeSplicingList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadRopeSplicingList"));

            }
        }


        public static ObservableCollection<RopeSplicingClass> loadUserAccess = new ObservableCollection<RopeSplicingClass>();
        public ObservableCollection<RopeSplicingClass> LoadUserAccess
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

        //private ObservableCollection<RopeSplicingClass> GetRopeSplicingList()
        //{

        //    try
        //    {
        //        ObservableCollection<RopeSplicingClass> rpsplcinglist = new ObservableCollection<RopeSplicingClass>();

        //        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
        //        SqlCommand cmd = new SqlCommand("GetRopeSplicing", sc.con);
        //        cmd.CommandType = CommandType.StoredProcedure;


        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //        DataTable ds = new DataTable();
        //        adp.Fill(ds);
        //        foreach (DataRow row in ds.Rows)
        //        {
        //            rpsplcinglist.Add(new RopeSplicingClass()
        //            {
        //                Id = (int)row["Id"],
        //                AssignedNumber = (string)row["AssignedNumber"],
        //                CertificateNumber = (string)row["CertificateNumber"],
        //                AssignedLocation = (string)row["AssignedLocation"],
        //                SplicingDoneDate = (DateTime)row["SplicingDoneDate"],                       
        //              SplicingDoneBy = (row["SplicingDoneBy"] == DBNull.Value) ? string.Empty : row["SplicingDoneBy"].ToString(),
        //                SplicingMethod = (row["SplicingMethod"] == DBNull.Value) ? string.Empty : row["SplicingMethod"].ToString(),

        //            });
        //        }

        //        return rpsplcinglist;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        return null;
        //    }

        //}

        public void GetRopeSplicingList()
        {

            try
            {
                LoadUserAccess.Clear();
                //ObservableCollection<RopeSplicingClass> rpsplcinglist = new ObservableCollection<RopeSplicingClass>();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                SqlCommand cmd = new SqlCommand("GetRopeSplicing", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RopeTail", 1);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess.Add(new RopeSplicingClass()
                    {
                        Id = (int)row["Id"],
                        //AssignedNumber = (string)row["AssignedNumber"],
                        AssignedNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                        CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? "Not Assigned" : row["CertificateNumber"].ToString(),
                        // CertificateNumber = (string)row["CertificateNumber"],
                        UniqueId = (row["UniqueId"] == DBNull.Value) ? "Not Assigned" : row["UniqueId"].ToString(),
                        AssignedLocation = (row["AssignedLocation"] == DBNull.Value) ? "Not Assigned" : row["AssignedLocation"].ToString(),
                           //AssignedLocation = (string)row["AssignedLocation"],
                           //SplicingDoneDate = (DateTime)row["SplicingDoneDate"],

                           SplicingDoneDate1 = ((string)row["SplicingDoneDate"]),
                           //SplicingDoneDate1 = ((DateTime)row["SplicingDoneDate"]).ToString("yyyy-MM-dd"),
                        SplicingDoneBy = (row["SplicingDoneBy"] == DBNull.Value) ? string.Empty : row["SplicingDoneBy"].ToString(),
                        SplicingMethod = (row["SplicingMethod"] == DBNull.Value) ? string.Empty : row["SplicingMethod"].ToString(),


                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }

        }

        private void EditCrossShiftingWinch(RopeSplicingClass mw)
        {
            try
            {
                //AddRopeEndtoEndViewModel vm = new AddRopeEndtoEndViewModel(mw);
                //ChildWindowManager.Instance.ShowChildWindow(new AssignRopeToWinchView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteCrossShiftingWinch(RopeSplicingClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RopeSplicingClass findcrs = sc.RopeSplicing.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findcrs != null)
                    {

                        //var checkDepartment = sc.AssignRopetoWinch.Where(x => x.Id.Equals(mw.Id)).FirstOrDefault();
                        //if (checkDepartment != null)
                        //{
                        //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                        //}
                        //else
                        //{

                        sc.Entry(findcrs).State = EntityState.Deleted;
                        sc.SaveChanges();

                        MessageBox.Show("Record deleted successfully ", "Delete CrossShifting", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........


                        //LoadMooringWinchList.Clear();

                        var lostdata = new ObservableCollection<RopeSplicingClass>(sc.RopeSplicing.ToList());
                        TailSplicingViewModel cc = new TailSplicingViewModel(lostdata);
                        //GetRopeSplicingList();
                        //sc.ObservableCollectionList(LoadMooringWinchList, GetEndtoEndList);

                        //.....End Refresh DataGrid........
                        //}
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
        private void ExportTailSplicingMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                ExportTailSplicingMethod1();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void ExportTailSplicingMethod1()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ViewRopeSplicingExcel", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RopeTail", 1);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "RopeTailSplicing_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (sfd.ShowDialog() == true)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch
                        {
                            MessageBox.Show("It seems that the earlier excel file is open, please close the excel file and download again to replace !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {

                        dt.TableName = "Tail Splicing";
                        var protectedsheet = wb.Worksheets.Add(dt);
                        protectedsheet.Name = dt.TableName;
                        var projection = protectedsheet.Protect("49WEB$TREET#");
                        projection.InsertColumns = true;
                        projection.InsertRows = true;

                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;
                        wb.SaveAs(sfd.FileName);
                        MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
