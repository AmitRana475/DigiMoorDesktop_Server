using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.Views;

namespace WorkShipVersionII.ViewModel
{
    public class NotificationCommentsViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public NotificationCommentsViewModel(NotificationCommentsClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<NotificationCommentsClass>(UpdateNotiCmnt);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            EditCmnt(edeps);
        }
        public NotificationCommentsViewModel(int Id, int type)
        {
            StaticHelper.Editing = true;

            StaticHelper.NotificationId = Id;
            StaticHelper.CommentsType = type;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            CheckReadComment(Id);

            NotificationCommentsClass notif = new NotificationCommentsClass();
            notif.CommentsType = type;
            saveCommand = new RelayCommand<NotificationCommentsClass>(SaveComment);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            editCommand = new RelayCommand<NotificationCommentsClass>(EditCmnt);
            //deleteCommand = new RelayCommand<NotificationCommentsClass>(Deleteropedamage);
            loadUserAccess.Clear();
            LoadUserAccess = GetNotiCmntList();

            if(StaticHelper.CommentsType == 1)
            {
                commenttitle = "Ship Comments";
            }
            else 
            {
                commenttitle = "Office Comments";
            }
        }

        private void CheckReadComment(int notiid)
        {
            try
            {
                //SqlDataAdapter adp = new SqlDataAdapter("update NotificationComment set IsRead='true' where CommentsType = 2 and NotificationId="+ notiid + "",sc.con);
                SqlDataAdapter adp = new SqlDataAdapter("UpdateNotificationComment", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@NotificationId", notiid);
                adp.SelectCommand.Parameters.AddWithValue("@IsRead",true);
                DataTable dt = new DataTable();
                adp.Fill(dt);
            }
            catch { }
        }

        private string commenttitle;

        public string CommentTitle
        {
            get {
                

                return commenttitle;
            }
            set
            {
                commenttitle = value;
                RaisePropertyChanged("CommentTitle");
            }
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


        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        private NotificationCommentsClass _NotiCmnt = new NotificationCommentsClass();
        public NotificationCommentsClass NotificationCmnt
        {
            get
            {
                MooringWinchMessage = string.Empty;
                RaisePropertyChanged("MooringWinchMessage");
                return _NotiCmnt;
            }
            set
            {
                _NotiCmnt = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NotificationCmnt"));
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

        public static ObservableCollection<NotificationCommentsClass> loadUserAccess = new ObservableCollection<NotificationCommentsClass>();
        public ObservableCollection<NotificationCommentsClass> LoadUserAccess
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
        private ObservableCollection<NotificationCommentsClass> GetNotiCmntList()
        {

            try
            {
                ObservableCollection<NotificationCommentsClass> cropelist = new ObservableCollection<NotificationCommentsClass>();

                SqlCommand cmd = new SqlCommand("NotificationCmntList", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotifiId", StaticHelper.NotificationId);
                cmd.Parameters.AddWithValue("@Action", StaticHelper.CommentsType);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    cropelist.Add(new NotificationCommentsClass()
                    {
                        Id = (int)row["Id"],
                        Comments = (string)row["Comments"],
                        CreatedDate = (DateTime)row["CreatedDate"],

                    });
                }

                return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return null;
            }

        }
        private void SaveComment(NotificationCommentsClass cmnt)
        {
            try
            {
                cmnt.NotificationId = StaticHelper.NotificationId;
                cmnt.CommentsType = StaticHelper.CommentsType;

                cmnt.Comments = cmnt.Comments;

                cmnt.CreatedDate = DateTime.Now;
                cmnt.CreatedBy = "Admin";
                cmnt.IsActive = true;

                sc.NotificationCommnent.Add(cmnt);
                sc.SaveChanges();
                StaticHelper.Editing = false;
                MessageBox.Show("Record saved successfully ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                NotificationCmnt = new NotificationCommentsClass();
                RaisePropertyChanged("NotificationCmnt");

                NotificationCommentsViewModel vm = new NotificationCommentsViewModel(StaticHelper.NotificationId, StaticHelper.CommentsType);
                ChildWindowManager.Instance.ShowChildWindow(new NotificationCommentsView() { DataContext = vm });



            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void UpdateNotiCmnt(NotificationCommentsClass cmnt)
        {
            try
            {
                //moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                //if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
                //{

                var findrank = sc.NotificationCommnent.Where(x => x.Id == cmnt.Id).FirstOrDefault();

                if (findrank != null)
                {
                    cmnt.Comments = cmnt.Comments;



                    var local = sc.Set<MooringWinchClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == cmnt.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    var UpdatedCmnt = new NotificationCommentsClass()
                    {

                        Id = cmnt.Id,
                        Comments = cmnt.Comments
                    };

                    sc.Entry(UpdatedCmnt).State = EntityState.Modified;
                    sc.SaveChanges();


                    //Update into User's Table
                    //var user = sc.CrewDetails.Where(x => x.did.Equals(UpdatedLocation.Id)).ToList();
                    //var depat = user.Where(x => x.did.Equals(UpdatedLocation.Id)).FirstOrDefault().department;
                    //user.ForEach(a =>
                    //{
                    //    a.department = UpdatedLocation.AssignedNumber;
                    //});

                    //sc.SaveChanges();

                    //Update into WorkHours's Table
                    //var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
                    //some.ForEach(a =>
                    //{
                    //    a.Department = UpdatedLocation.AssignedNumber;
                    //});

                    //sc.SaveChanges();
                    //StaticHelper.Editing = false;
                    MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);


                    //CancelMooringWinch();

                    // }

                }
                else
                {

                    MooringWinchMessage = "Please Enter the MooringWinch Name";
                    RaisePropertyChanged("MooringWinchMessage");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void EditCmnt(NotificationCommentsClass moorwinch)
        {
            try
            {

                var findcmnt = sc.NotificationCommnent.Where(x => x.Id == moorwinch.Id).FirstOrDefault();
                NotificationCmnt.Comments = findcmnt.Comments;
                //AddMooringWinch.Location = findrank.Location;
                //AddMooringWinch.Id = findrank.Id;
                OnPropertyChanged(new PropertyChangedEventArgs("NotificationCmnt"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelMooringWinch()
        {
           // var lostdata = new ObservableCollection<NotificationsClass>(sc.Notifications.ToList());
            NotificationsViewModel cc = new NotificationsViewModel();

            cc.GetNotificationList();


            
           // MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);
            //ChildWindowManager.Instance.CloseChildWindow();
            //new MooringWinchViewModel();
            //new CrewDetailViewModel();
            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        //public void OnPropertyChanged(PropertyChangedEventArgs e)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, e);
        //}


    }
}
