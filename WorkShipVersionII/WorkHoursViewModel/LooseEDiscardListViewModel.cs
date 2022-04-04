using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;


namespace WorkShipVersionII.WorkHoursViewModel
{
    public class LooseEDiscardListViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _ExportLooseEDiscardList;
        public ICommand HelpCommand { get; private set; }
        public LooseEDiscardListViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                _ExportLooseEDiscardList = new BackgroundWorker();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            viewCommand = new RelayCommand<RopeDiscardRecordClass>(Viewropediscard);
            editCommand = new RelayCommand<RopeDiscardRecordClass>(EditRopeDiscard);
            deleteCommand = new RelayCommand<RopeDiscardRecordClass>(DeleteRopeDiscard);

            this._ExportLooseEDiscardListCommand = new RelayCommand(() => _ExportLooseEDiscardList.RunWorkerAsync(), () => !_ExportLooseEDiscardList.IsBusy);
            this._ExportLooseEDiscardList.DoWork += new DoWorkEventHandler(ExportLooseEDiscardListMethod);


            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetLooseEDiscardList();


        }
        private void Viewropediscard(RopeDiscardRecordClass mw)
        {
            try
            {
                //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                StaticHelper.ViewId = mw.LooseETypeId;
                StaticHelper.SearchText = mw.CertificateNumber;
                ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEDiscard());

                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void EditRopeDiscard(RopeDiscardRecordClass mw)
        {
            try
            {
                RopeDiscardRecordViewModel vm = new RopeDiscardRecordViewModel(mw);

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private ICommand _ExportLooseEDiscardListCommand;
        public ICommand ExportLooseEDiscardListCommand
        {
            get
            {
                return _ExportLooseEDiscardListCommand;
            }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
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
        public LooseEDiscardListViewModel(ObservableCollection<RopeDiscardRecordClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetLooseEDiscardList();

        }



        public static ObservableCollection<RopeDiscardRecordClass> loadUserAccess = new ObservableCollection<RopeDiscardRecordClass>();
        public ObservableCollection<RopeDiscardRecordClass> LoadUserAccess
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

        public void GetLooseEDiscardList()
        {
            try
            {
                LoadUserAccess.Clear();
                SqlCommand cmd = new SqlCommand("GetLooseEDiscardList", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RopeTail", 0);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess.Add(new RopeDiscardRecordClass()
                    {
                        Id = (int)row["Id"],
                        LooseETypeId = (int)row["LooseETypeId"],
                        looseequipmenttype = (string)row["looseequipmenttype"],
                        CertificateNumber = (string)row["CertificateNumber"],
                        UniqueId = (string)row["UniqueID"],
                        //OutofServiceDate = (DateTime)row["outofservicedate"],
                        OutofServiceDate1 = ((string)row["OutofServiceDate"]),
                        //OutofServiceDate1 = ((DateTime)row["OutofServiceDate"]).ToString("yyyy-MM-dd"),
                        //OutofServiceDate = ((DateTime)row["OutofServiceDate"]),
                        ReasonOutofService = (row["reasonoutofservice"] == DBNull.Value) ? string.Empty : row["reasonoutofservice"].ToString(),

                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }

        private void DeleteRopeDiscard(RopeDiscardRecordClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string tablename = "";
                    if (mw.LooseETypeId == 1)
                    {
                        tablename = "JoiningShackle";
                    }
                    if (mw.LooseETypeId == 3 || mw.LooseETypeId == 4 || mw.LooseETypeId == 6)
                    {
                        tablename = "RopeTail";

                    }
                    if (mw.LooseETypeId == 5)
                    {
                        tablename = "ChainStopper";
                    }
                    if (mw.LooseETypeId == 7)
                    {
                        tablename = "ChafeGuard";

                    }
                    if (mw.LooseETypeId == 8)
                    {
                        tablename = "WinchBreakTestKit";
                    }

                    SqlDataAdapter adp = new SqlDataAdapter("select * from LooseEDisposal where IsActive=1 and LooseETypeId=" + mw.LooseETypeId + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("You can not delete this record ! Firstly remove from Loose Eq. Disposal. ", "Delete Loose Eq.", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (mw.LooseETypeId == 1)
                        {
                            var local = sc.Set<JoiningShackleClass>()
                    .Local
                    .FirstOrDefault(f => f.Id == mw.Id);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }

                            var result = sc.JoiningShackles.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                            if (result != null)
                            {

                                result.OutofServiceDate = null;
                                result.ModifiedDate = DateTime.Now;
                                sc.Entry(result).State = EntityState.Modified;
                                sc.SaveChanges();
                            }
                        }
                        if (mw.LooseETypeId == 3 || mw.LooseETypeId == 4 || mw.LooseETypeId == 6 || mw.LooseETypeId == 9 || mw.LooseETypeId == 10)
                        {
                            var local = sc.Set<RopeTailClass>()
                    .Local
                    .FirstOrDefault(f => f.Id == mw.Id);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }
                            var result = sc.RopeTails.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                            if (result != null)
                            {

                                result.OutofServiceDate = null;
                                result.ModifiedDate = DateTime.Now;
                                sc.Entry(result).State = EntityState.Modified;
                                sc.SaveChanges();
                            }
                        }

                        if (mw.LooseETypeId == 5)
                        {
                            var local = sc.Set<ChainStopperClass>()
                    .Local
                    .FirstOrDefault(f => f.Id == mw.Id);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }

                            var result = sc.ChainStoppers.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                            if (result != null)
                            {

                                result.OutofServiceDate = null;
                                result.ModifiedDate = DateTime.Now;
                                sc.Entry(result).State = EntityState.Modified;
                                sc.SaveChanges();
                            }
                        }
                        if (mw.LooseETypeId == 7)
                        {
                            var local = sc.Set<ChafeGuardClass>()
                  .Local
                  .FirstOrDefault(f => f.Id == mw.Id);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }
                            var result = sc.ChafeGuard.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                            if (result != null)
                            {

                                result.OutofServiceDate = null;
                                sc.Entry(result).State = EntityState.Modified;
                                sc.SaveChanges();
                            }
                        }
                        if (mw.LooseETypeId == 8)
                        {
                            var local = sc.Set<WBTestKitClass>()
                  .Local
                  .FirstOrDefault(f => f.Id == mw.Id);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }
                            var result = sc.WBTestKit.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                            if (result != null)
                            {

                                result.OutofServiceDate = null;
                                sc.Entry(result).State = EntityState.Modified;
                                sc.SaveChanges();
                            }
                        }

                        try
                        {

                            //var result1 = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == mw.RopeId);
                            //if (result1 != null)
                            //{

                            //    result1.IsActive = true;
                            //    result1.ModifiedDate = DateTime.Now;
                            //    sc.SaveChanges();
                            //}

                            //var result11 = sc.RopeDamage.SingleOrDefault(b => b.RopeId == mw.RopeId);
                            //if (result11 != null)
                            //{

                            //    int ropeid = result11.RopeId;
                            //    int notifiid = Convert.ToInt32(result11.NotificationId);


                            //    result11.IsActive = true;
                            //    result11.ModifiedDate = DateTime.Now;
                            //    sc.SaveChanges();
                            //}

                            //SqlDataAdapter adp1 = new SqlDataAdapter("update notifications set IsActive=0 where ropeid=" + mw.Id + "", sc.con);
                            //DataTable dt1 = new DataTable();
                            //adp1.Fill(dt1);

                        }
                        catch { }
                        MessageBox.Show("Record deleted successfully ", "Delete Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                        LooseEDiscardListViewModel ss = new LooseEDiscardListViewModel();
                        ss.GetLooseEDiscardList();
                    }



                    //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList().Where(x => x.DeleteStatus == false));
                    //MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);


                    //var lostdata = new ObservableCollection<RopeDiscardRecordClass>(sc.MooringWinchRope.ToList().Where(x => x.DeleteStatus == false));
                    //MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);





                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void ExportLooseEDiscardListMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                ExportLooseEDiscardListMethod1();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void ExportLooseEDiscardListMethod1()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetLooseEDiscardListExcel", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RopeTail", 1);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dt.Columns.Remove("looseetypeid");
                dt.Columns.Remove("id");
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "LooseEquipmentDiscard_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                        dt.TableName = "Loose Equipment Discard";
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
