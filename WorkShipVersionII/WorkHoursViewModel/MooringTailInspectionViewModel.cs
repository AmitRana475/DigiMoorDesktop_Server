using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using WorkShipVersionII.WorkHoursView;
using WorkShipVersionII.ViewModel;
using ClosedXML.Excel;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class MooringTailInspectionViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public MooringTailInspectionViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            MooringTailInspectionView.imagesavelist.Clear();
            MooringTailInspectionView.imagesavelist1.Clear();
            saveCommand = new RelayCommand(SaveAllInspection);
            downloadexcelCommand = new RelayCommand(DownloadInspection);
             cancelCommand = new RelayCommand(CancelRopeInspection);

            
        HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

        LoadInspections.Clear();
            //  LoadInspections = GetMooringInspection();
            GetMooringInspection();


        }

        public MooringTailInspectionViewModel(TotalInspections total)
        {
            // Edit Operation
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            saveCommand = new RelayCommand(EditAllInspection);
            downloadexcelCommand = new RelayCommand(DownloadInspection);

            deletePhoto = new RelayCommand<MooringRopeInspectionClass>(DeleteImage);
            deletePhoto1 = new RelayCommand<MooringRopeInspectionClass>(DeleteImage1);

            viewCommand = new RelayCommand<MooringRopeInspectionClass>(Viewimage);

            viewCommand1 = new RelayCommand<MooringRopeInspectionClass>(Viewimage1);

            saveCommand = new RelayCommand(UpdateMooringTailInspection);
            LoadInspections.Clear();
            LoadInspections = GetMooringInspection(total);
            //GetMooringInspection();
        }
        public void resetform()
        {
            MooringInspect = new MooringRopeInspectionClass();
            RaisePropertyChanged("MooringInspect");

            EndtoEndDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("EndtoEndDoneDate");


            //  SRopeAss = null; RaisePropertyChanged("SRopeAss");
            //MooringInspect
        }

        private ICommand deletePhoto;
        public ICommand DeletePhoto
        {
            get { return deletePhoto; }
            set { deletePhoto = value; }
        }

        private ICommand deletePhoto1;
        public ICommand DeletePhoto1
        {
            get { return deletePhoto1; }
            set { deletePhoto1 = value; }
        }

        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }

        private ICommand viewCommand1;
        public ICommand ViewCommand1
        {
            get { return viewCommand1; }
            set { viewCommand1 = value; }
        }

        private void Viewimage(MooringRopeInspectionClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                StaticHelper.Photos = "Image1";
                StaticHelper.TbName = "MooringRopeInspection";
                ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());


            }
            catch (Exception ex)
            {

            }
        }

        private void DeleteImage(MooringRopeInspectionClass mw)
        {
            try
            {
                if (System.Windows.MessageBox.Show("Do you want to Delete this Image?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    List<MooringRopeInspectionClass> list = new List<MooringRopeInspectionClass>();
                    //SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image1=null,Photo1=null where ID= " + mw.Id + "", sc.con);
                    //System.Data.DataTable dt = new System.Data.DataTable();
                    //adp.Fill(dt);
                    SqlDataAdapter adp = new SqlDataAdapter("UpdateMooringRopeInspection", sc.con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adp.Fill(dt);

                    RaisePropertyChanged("LoadInspections2");

                    // LoadInspections2 = LoadInspections;
                    list = LoadInspections.ToList();

                                   string ServerName = StaticHelper.ServerName;
                                   string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                                   //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\";
                    MooringTailInspectionView.imagesavelist.ForEach(x => {
                        if (x.RopeId == mw.RopeId)
                        {

                            if (File.Exists(mypath + x.imagename1))
                            {
                                File.Delete(mypath + x.imagename1);
                            }
                            x.photo1 = null;
                            x.imagename1 = null;
                        }
                    });

                    list.ForEach(x => {
                        if (x.Id == mw.Id)
                        {
                            x.Photo11 = "Hidden";
                            x.Showbutton1 = "Visible";
                            x.Photo1 = null;
                            x.Image1 = null;
                            x.ButtonContent1 = "Browse";



                        }
                    });
                  
                    LoadInspections.Clear();
                    sc.ObservableCollectionList(LoadInspections, list);

                    RaisePropertyChanged("LoadInspections");

                }
            }
            catch (Exception ex)
            {

            }
        }

        //Observablecollection<Entity> bRef = new Observablecollection<Entity>(aRef);



        private void DeleteImage1(MooringRopeInspectionClass mw)
        {
            try
            {
                if (System.Windows.MessageBox.Show("Do you want to Delete this Image?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    List<MooringRopeInspectionClass> list = new List<MooringRopeInspectionClass>();
                    //SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image2=null,Photo2=null where ID= " + mw.Id + "", sc.con);
                    SqlDataAdapter adp = new SqlDataAdapter("UpdateMooringRopeInspection2", sc.con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adp.Fill(dt);


                    RaisePropertyChanged("LoadInspections2");

                    // LoadInspections2 = LoadInspections;
                    list = LoadInspections.ToList();

                                   string ServerName = StaticHelper.ServerName;
                                   string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                                   //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\";
                    MooringTailInspectionView.imagesavelist1.ForEach(x => {
                        if (x.RopeId == mw.RopeId)
                        {
                            if (File.Exists(mypath + x.imagename2))
                            {
                                File.Delete(mypath + x.imagename2);
                            }
                            x.photo2 = null;
                            x.imagename2 = null;


                        }
                    });

                    list.ForEach(x => {
                        if (x.Id == mw.Id)
                        {
                            x.Photo12 = "Hidden";
                            x.Showbutton2 = "Visible";
                            x.Photo2 = null;
                            x.Image2 = null;
                            x.ButtonContent2 = "Browse";

                            //StaticHelper. = mw.RopeId;
                            //photoimglist1 = null;
                        }
                    });

                    LoadInspections.Clear();
                    sc.ObservableCollectionList(LoadInspections, list);

                    RaisePropertyChanged("LoadInspections");

                    // LoadInspections = GetMooringInspection(tt);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Viewimage1(MooringRopeInspectionClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                StaticHelper.Photos = "Image2";
                StaticHelper.TbName = "MooringRopeInspection";
                ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());


            }
            catch (Exception ex)
            {

            }
        }
        private void CancelRopeInspection()
        {
            MainViewModelWorkHours.CommonValue = false;
            var lostdata = new ObservableCollection<MooringRopeInspectionClass>(sc.MooringRopeInspectionTbl.ToList());
            TailInspectionListViewModel cc = new TailInspectionListViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }
        private bool allSelected;
        public bool AllSelected
        {
            get
            {
                return allSelected;
            }
            set
            {
                try
                {
                    bool test = allSelected;
                    if (test == false)
                    {
                        allSelected = value;

                        SelectAllMooringInspection();
                    }
                    else
                    {
                        allSelected = value;
                        SelectAllMooringInspection();
                        //GetMooringInspection();
                    }

                }
                catch (Exception ex)
                {
                    sc.ErrorLog(ex);
                    allSelected = value;
                }
                // RaisePropertyChanged(() => this.PeopleList);

                RaisePropertyChanged("AllSelected");
            }

           
        }
        private void EditAllInspection()
        {

        }
        private void UpdateMooringTailInspection()
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                if (MooringInspect.InspectBy == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    System.Windows.MessageBox.Show("Please fill Inspected By !", "Rope Tail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int validationcheck = 0;
                int inspectioncheck = 0;
                string test = ""; int insdtcheck = 0;
                foreach (var item in LoadInspections)
                {
                    if (item.IsCheckedV == true)
                    {
                        inspectioncheck = 1;
                        //if (item.InternalRating_A == 0.00 || item.InternalRating_B == 0.00 || item.ExternalRating_A == 0.00 || item.ExternalRating_B == 0.00 || item.LengthOFAbrasion_A == 0 || item.LengthOFAbrasion_B == 0 || item.CutYarnCount_A == 0 || item.CutYarnCount_B == 0 || item.DistanceOutboard_A == 0 || item.DistanceOutboard_B == 0 || item.LengthOFGlazing_A == 0 || item.LengthOFGlazing_B == 0 || item.Chafe_guard_condition == null || item.Twist == 0)
                        if (item.InternalRating_A == 0.00 || item.ExternalRating_A == 0.00 || item.Chafe_guard_condition == null)
                        {
                            validationcheck = 1;
                        }


                        DateTime? insdt = DateTime.Now;
                        DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                        SqlDataAdapter adp10 = new SqlDataAdapter("select installeddate from mooringropedetail where ID=" + item.RopeId + "", sc.con);
                        System.Data.DataTable dt10 = new System.Data.DataTable();
                        adp10.Fill(dt10);
                        if (dt10.Rows.Count > 0)
                        {
                            insdt = Convert.ToDateTime(dt10.Rows[0][0] == DBNull.Value ? null : dt10.Rows[0][0]);
                        }
                        else
                        {
                            insdt = null;
                        }
                        if (inspectdt < insdt)
                        {
                            insdtcheck = 1;
                            if (test == "")
                            {
                                test = item.Certi_No;
                            }
                            else
                            {
                                test = test + "," + item.Certi_No;
                            }

                        }
                    }

                    SqlDataAdapter adp = new SqlDataAdapter("delete from mooringropeinspection where inspectionid=" + item.InspectionId + "", sc.con);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adp.Fill(dt);
                }

                if (validationcheck == 1)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    System.Windows.MessageBox.Show("Please fill all fields !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (inspectioncheck == 0)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    System.Windows.MessageBox.Show("Please choose at least 1 Tail !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if(insdtcheck==0)
                { 
                decimal avg = 0; 
                var Ropelist = sc.MooringWinchRope.Where(x => x.DeleteStatus == false && x.OutofServiceDate == null && x.RopeTail==1).ToList();
                var InspecSetting = sc.RopeTailInspectionSetting.ToList();
                int nxtinspctid = NextInspectionId();
                var photoimglist = MooringTailInspectionView.imagesavelist.ToList();
                var photoimglist1 = MooringTailInspectionView.imagesavelist1.ToList();
                    foreach (var item in LoadInspections)
                    {
                        if (item.IsCheckedV == true)
                        {


                            int notiid = sc.NextNotiId();
                            var ss = item;
                            ss.InspectDate = MooringInspect.InspectDate;
                            ss.InspectBy = MooringInspect.InspectBy;


                            var imagename1 = photoimglist.Where(x => x.RopeId == item.RopeId).Select(x => x.imagename1).SingleOrDefault();
                            var photoname1 = photoimglist.Where(x => x.RopeId == item.RopeId).Select(x => x.photo1).SingleOrDefault();


                            var imagename2 = photoimglist1.Where(x => x.RopeId == item.RopeId).Select(x => x.imagename2).SingleOrDefault();
                            var photoname2 = photoimglist1.Where(x => x.RopeId == item.RopeId).Select(x => x.photo2).SingleOrDefault();


                            //ss.Photo1 = photoname1;
                            //ss.Photo2 = photoname2;
                            //ss.Image1 = imagename1;
                            //ss.Image2 = imagename2;

                            //ss.Photo1 = MooringInspect.Photo1;
                            //ss.Photo2 = MooringInspect.Photo2;
                            //ss.Image1 = MooringInspect.Image1;
                            //ss.Image2 = MooringInspect.Image2;


                            ss.Photo1 = photoname1;
                            ss.Photo2 = photoname2;
                            ss.Image1 = imagename1;
                            ss.Image2 = imagename2;

                            ss.RopeId = item.RopeId;
                            ss.WinchId = item.WinchId;

                            ss.IsActive = true;
                            if (ss.InspectBy == null)
                            {
                                ss.InspectBy = "Admin";
                            }
                            ss.RopeTail = 1;
                            ss.NotificationId = notiid;
                            ss.InspectionId = nxtinspctid;
                                                 ss.CreatedDate = DateTime.Now;
                                                 ss.CreatedBy = "Admin";


                                                 sc.MooringRopeInspectionTbl.Add(ss);


                            if (insdtcheck == 0)
                            {

                                int firstvalue = Convert.ToInt32(item.ExternalRating_A);
                                int secondvalue = Convert.ToInt32(item.InternalRating_A);
                                decimal finalvalue = (firstvalue + secondvalue);
                                finalvalue = finalvalue / 2;

                                finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                                item.AverageRating_A = Convert.ToInt32(finalvalue);

                                decimal avRA = item.AverageRating_A;

                                avg = avRA;


                                var RopeId = Ropelist.Where(x => x.Id == item.RopeId && x.RopeTail == 1).FirstOrDefault();

                                string isTail = RopeId.RopeTail == 1 ? "Tail" : string.Empty;

                                var certinumber = Ropelist.Where(x => x.Id == item.RopeId && x.OutofServiceDate == null && x.DeleteStatus == false).Select(x => x.UniqueID).SingleOrDefault();

                                if (avg >= 5 && avg < 6)
                                {
                                    var Rating_5rope = "";
                                    if (item.AssignNumber != null || item.AssignNumber != "")
                                    {
                                        Rating_5rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                    }
                                    else
                                    {
                                        Rating_5rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + "";
                                    }
                                    //var Rating_5rope = "Rating 5 - Rope #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;

                                    if (MooringInspect.Image1 == null && MooringInspect.Image2 == null)
                                    {
                                        MainViewModelWorkHours.CommonValue = true;
                                        //System.Windows.MessageBox.Show("Due to the reported condition of Line ID {" + certinumber + "}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                        System.Windows.MessageBox.Show("Photo Can not be blank for certificate number '" + certinumber + "' ", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                        return;
                                    }
                                    else
                                    {
                                        int NotiAlertType = (int)NotificationAlertType.Rating_5Rope;
                                        InspectNotification(item.RopeId, Rating_5rope, NotiAlertType, DateTime.Now.Date);

                                    }



                                }
                                else if (avg >= 6 && avg < 7)
                                {
                                    var Rating_6rope = "";
                                    if (item.AssignNumber != null || item.AssignNumber != "")
                                    {
                                        Rating_6rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                    }
                                    else
                                    {
                                        Rating_6rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + "";
                                    }
                                    //var Rating_5rope = "Rating 5 - Rope #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;

                                    if (MooringInspect.Image1 == null && MooringInspect.Image2 == null)
                                    {
                                        MainViewModelWorkHours.CommonValue = true;

                                        System.Windows.MessageBox.Show("Due to the reported condition of Line ID {"+ certinumber +"}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                        //System.Windows.MessageBox.Show("Photo Can not be blank for certificate number '" + certinumber + "' ", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                        return;
                                    }
                                    else
                                    {
                                        int NotiAlertType = (int)NotificationAlertType.Rating_5Rope;
                                        InspectNotification(item.RopeId, Rating_6rope, NotiAlertType, DateTime.Now.Date);

                                    }


                                }
                                else if (avg >= 7)
                                {

                                    var Rating_7rope = "";
                                    if (item.AssignNumber != null || item.AssignNumber != "")
                                    {
                                        Rating_7rope = "Rating 7 - Rope" + isTail + " #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                    }
                                    else
                                    {
                                        Rating_7rope = "Rating 7 - Rope" + isTail + " #" + certinumber + "";
                                    }
                                    // var Rating_7rope = "Rating 7 - Rope #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;

                                    if (MooringInspect.Image1 == null && MooringInspect.Image2 == null)
                                    {
                                        MainViewModelWorkHours.CommonValue = true;
                                        System.Windows.MessageBox.Show("Due to the reported condition of Line ID {" + certinumber + "}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                        //System.Windows.MessageBox.Show("Photo Can not be blank for certificate number '" + certinumber + "' ", "Rope Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                        return;
                                    }
                                    else
                                    {
                                        int NotiAlertType = (int)NotificationAlertType.Rating_7Rope;
                                        InspectNotification3(item.RopeId, Rating_7rope, NotiAlertType, DateTime.Now.Date);

                                    }


                                }

                                var ratingcheck = InspecSetting.Where(x => x.MooringRopeType == RopeId.RopeTypeId && x.ManufacturerType == RopeId.ManufacturerId).FirstOrDefault();

                                if (ratingcheck != null)
                                {
                                    decimal[] array = new decimal[7] { ratingcheck.Rating1, ratingcheck.Rating2, ratingcheck.Rating3, ratingcheck.Rating4, ratingcheck.Rating5, ratingcheck.Rating6, ratingcheck.Rating7 };

                                    var nearest = array.OrderBy(x => Math.Abs((long)x - avg)).First();
                                    int near = Convert.ToInt32(nearest);
                                    string rating = "Rating" + avg;
                                    SqlDataAdapter pp = new SqlDataAdapter("select " + rating + " from tblRopeTailInspectionSetting where mooringropetype=" + RopeId.RopeTypeId + " and manufacturertype=" + RopeId.ManufacturerId + "", sc.con);
                                    System.Data.DataTable rtc = new System.Data.DataTable();
                                    pp.Fill(rtc);
                                    if (rtc.Rows.Count > 0)
                                    {
                                        decimal perchk = Convert.ToDecimal(rtc.Rows[0][0]) * 30 / 100;
                                        perchk = perchk * 100;
                                        //near = Convert.ToInt32(rtc.Rows[0][0]);
                                        near = Convert.ToInt32(perchk);
                                        // near = Convert.ToInt32(rtc.Rows[0][0]);
                                    }

                                    DateTime notidueMonth = Convert.ToDateTime(ss.InspectDate).AddDays(near);

                                    var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == item.RopeId && b.IsActive == true && b.DeleteStatus == false && b.RopeTail == 1);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = notidueMonth;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        // sc.SaveChanges();

                                    }

                                    //var certinumber = Ropelist.Where(x => x.Id == item.RopeId && x.RopeTail == 1).Select(x => x.CertificateNumber).SingleOrDefault();
                                    //var notification = "";
                                    //if (item.AssignNumber != null && item.AssignNumber != "")
                                    //{
                                    //    notification = "Inspection Due on 7 days- RopeTail #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                    //}
                                    //else
                                    //{
                                    //    notification = "Inspection Due on 7 days- RopeTail #" + certinumber + "";
                                    //}
                                    ////var notification = "Inspection Due on 7 days- RopeTail #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                    //NotificationsClass noti = new NotificationsClass();
                                    //noti.Acknowledge = false;
                                    //noti.AckRecord = "Not yet acknowledged";
                                    //noti.Notification = notification;
                                    //noti.NotificationDueDate = notidueMonth;
                                    //noti.RopeId = item.RopeId;
                                    //noti.IsActive = true;
                                    //noti.CreatedDate = DateTime.Now;
                                    //noti.CreatedBy = "Admin";
                                    //sc.Notifications.Add(noti);
                                }

                            }
                        }
                    }
                }
                if (insdtcheck == 0)
                {
                    sc.SaveChanges();
                    System.Windows.MessageBox.Show("Record Updated successfully", "Tail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    CancelRopeInspection();
                }
                else
                {
                    MainViewModelWorkHours.CommonValue = true;
                    System.Windows.MessageBox.Show("The inspection date is earlier than installed date for these ropetail " + test + " , Kindly change the inspection date or installed dates of these ropes from rope detail form", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch { }
        }


        public void InspectNotification(int RopeID, string NotiMsg, int NotiAlertType, DateTime DueDate)
        {
            //var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationDueDate == DueDate & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                noti.AckRecord = "Not yet acknowledged";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DueDate;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                sc.Notifications.Add(noti);
               // sc.SaveChanges();


                StaticHelper.AlarmFunction(1, true);
            }
        }

        public void InspectNotification3(int RopeID, string NotiMsg, int NotiAlertType, DateTime DueDate)
        {
            //var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationDueDate == DueDate & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                //noti.AckRecord = "Not yet acknowledged";
                noti.AckRecord = "This notification cannot be acknowledged, kindly discard it";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DueDate;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                sc.Notifications.Add(noti);
               // sc.SaveChanges();

                StaticHelper.AlarmFunction(1, true);

            }
        }


        private BackgroundWorker _Worker; int ib = 1;
        private void DownloadInspection()
        {

            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "DigiMoor_X7_InspectionSheet_" + DateTime.Now.ToString("dd-MMM-yyyy");
                dlg.DefaultExt = "xlsx";
                dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(dlg.FileName))
                    {
                        File.Delete(dlg.FileName);
                    }


                    int ib = 1;
                    //_Worker.ReportProgress(ib);
                    //PVisible = "Visible";
                    //...............


                    DataSet dataSet = new DataSet();
                    //dataSet = new DataSet("General");

                    //_Worker.ReportProgress(ib + 1);



                    string qry = "RopeInspection";
                    SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                    System.Data.DataTable datatbl = new System.Data.DataTable();

                    sda.Fill(datatbl);

                    datatbl.Columns.Remove("WinchId");
                    datatbl.Columns.Remove("RopeId");

                    datatbl.Columns.Add("External", typeof(string));
                    datatbl.Columns.Add("Internal", typeof(string));
                    datatbl.Columns.Add("Average", typeof(string));
                    datatbl.Columns.Add("Length_of_Abrasion", typeof(string));
                    datatbl.Columns.Add("Distance_from_outboard_eye", typeof(string));
                    datatbl.Columns.Add("Cut_yard_counted", typeof(string));
                    datatbl.Columns.Add("Length_of_glazing", typeof(string));

                    //datatbl.Columns.Add("External_B", typeof(string));
                    //datatbl.Columns.Add("Internal_B", typeof(string));
                    //datatbl.Columns.Add("Average_B", typeof(string));
                    //datatbl.Columns.Add("Length_of_Abrasion_B", typeof(string));
                    //datatbl.Columns.Add("Distance_from_outboard_eye_B", typeof(string));
                    //datatbl.Columns.Add("Cut_yard_counted_B", typeof(string));
                    //datatbl.Columns.Add("Length_of_glazing_B", typeof(string));

                    datatbl.Columns.Add("Chafe_Guard", typeof(string));
                    datatbl.Columns.Add("Twist", typeof(string));

                    dataSet.Tables.Add(datatbl);

                    //dataSet = datatbl

                    var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable item in dataSet.Tables)
                        {
                            var mytbl = item.TableName;
                            var protectedsheet = wb.Worksheets.Add(item);
                            protectedsheet.Name = item.TableName;
                            var projection = protectedsheet.Protect("49WEB$TREET#");
                            projection.InsertColumns = true;
                            projection.InsertRows = true;

                            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wb.Style.Font.Bold = true;
                        }
                        wb.SaveAs(dlg.FileName);
                        //  MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);



                    }

                    //ApplicationClass ExcelApp = new ApplicationClass();
                    //ExcelApp.Application.Workbooks.Add(Type.Missing);
                    //Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                    //// Loop over DataTables in DataSet.
                    //DataTableCollection collection = dataSet.Tables;
                    //dataSet.Dispose();

                    //var a = ib;
                    //var cons = collection.Count;
                    //for (int i = cons; i > 0; i--)
                    //{
                    //    //ib = ib + i;
                    //    //_Worker.ReportProgress(ib * (100 / cons + a));

                    //    Sheets xlSheets = null;
                    //    Worksheet xlWorksheet = null;
                    //    //Create Excel Sheets
                    //    xlSheets = ExcelApp.Sheets;
                    //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                    //                   Type.Missing, Type.Missing, Type.Missing);

                    //    System.Data.DataTable table = collection[i - 1];
                    //    xlWorksheet.Name = table.TableName;
                    //    var tcoun = table.Columns.Count;
                    //    for (int j = 1; j < tcoun + 1; j++)
                    //    {
                    //        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    //    }


                    //    // Storing Each row and column value to excel sheet
                    //    var rcoun = table.Rows.Count;
                    //    var rcoun1 = table.Columns.Count;
                    //    for (int k = 0; k < rcoun; k++)
                    //    {

                    //        for (int l = 0; l < rcoun1; l++)
                    //        {
                    //            ExcelApp.Cells[k + 2, l + 1] =
                    //            table.Rows[k].ItemArray[l].ToString();
                    //        }


                    //    }

                    //    ExcelApp.Columns.AutoFit();
                    //    ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[xlWorksheet.Name]).Protect("49WEB$TREET#");

                    //}

                    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();

                    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).SaveAs(dlg.FileName);
                    //ExcelApp.ActiveWorkbook.Saved = true;
                    //ExcelApp.Quit();

                    System.Windows.MessageBox.Show("Excel file download succefully !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch 
            {
                //sc.ErrorLog(ex);

               // System.Windows.MessageBox.Show("Please close excel file !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        private ICommand downloadexcelCommand;
        public ICommand DownloadExcelCommand
        {
            get { return downloadexcelCommand; }
        }
        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }


        public void SaveAllInspection()
        {
            MainViewModelWorkHours.CommonValue = false;
            if (MooringInspect.InspectBy == null)
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("Please fill Inspected By !", "Rope Tail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int validationcheck = 0;
            int inspectioncheck = 0; string test = ""; int insdtcheck = 0;

            foreach (var item in LoadInspections)
            {
                if (item.IsCheckedV == true)
                {
                    inspectioncheck = 1;
                    //if (item.InternalRating_A == 0.00 || item.ExternalRating_A == 0.00 || item.LengthOFAbrasion_A == 0 || item.CutYarnCount_A == 0 || item.DistanceOutboard_A == 0 || item.LengthOFGlazing_A == 0 || item.Chafe_guard_condition == null || item.Twist == 0)
                    if (item.InternalRating_A == 0.00 || item.ExternalRating_A == 0.00 || item.Chafe_guard_condition == null)
                        {
                        validationcheck = 1;
                    }

                    DateTime? insdt = DateTime.Now;
                    DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                    SqlDataAdapter adp = new SqlDataAdapter("select installeddate from mooringropedetail where ID=" + item.RopeId + "", sc.con);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        insdt = Convert.ToDateTime(dt.Rows[0][0] == DBNull.Value ? null : dt.Rows[0][0]);
                    }
                    else
                    {
                        insdt = null;
                    }
                    if (inspectdt < insdt)
                    {
                        insdtcheck = 1;
                        if (test == "")
                        {
                            test = item.Certi_No;
                        }
                        else
                        {
                            test = test + "," + item.Certi_No;
                        }

                    }

                }
            }

            if (validationcheck == 1)
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("Please fill all fields !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (inspectioncheck == 0)
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("Please choose atleast 1 RopeTail to inspect by selecting checkbox in the first column !", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(insdtcheck==0)
            { 
            decimal avg = 0; 
            var Ropelist = sc.MooringWinchRope.Where(x=> x.DeleteStatus== false && x.OutofServiceDate == null && x.RopeTail == 1).ToList();
            var InspecSetting = sc.RopeTailInspectionSetting.ToList();
            int nxtinspctid = NextInspectionId();
            var photoimglist = MooringTailInspectionView.imagesavelist.ToList();
            var photoimglist1 = MooringTailInspectionView.imagesavelist1.ToList();
                foreach (var item in LoadInspections)
                {
                    if (item.IsCheckedV == true)
                    {
                        int notiid = sc.NextNotiId();
                        var ss = item;
                        ss.InspectDate = MooringInspect.InspectDate;
                        ss.InspectBy = MooringInspect.InspectBy;

                        var imagename1 = photoimglist.Where(x => x.RopeId == item.RopeId && x.WinchId == item.WinchId).Select(x => x.imagename1).SingleOrDefault();
                        var photoname1 = photoimglist.Where(x => x.RopeId == item.RopeId && x.WinchId == item.WinchId).Select(x => x.photo1).SingleOrDefault();


                        var imagename2 = photoimglist1.Where(x => x.RopeId == item.RopeId && x.WinchId == item.WinchId).Select(x => x.imagename2).SingleOrDefault();
                        var photoname2 = photoimglist1.Where(x => x.RopeId == item.RopeId && x.WinchId == item.WinchId).Select(x => x.photo2).SingleOrDefault();


                        //ss.Photo1 = MooringInspect.Photo1;
                        //ss.Photo2 = MooringInspect.Photo2;
                        //ss.Image1 = MooringInspect.Image1;
                        //ss.Image2 = MooringInspect.Image2;


                        ss.Photo1 = photoname1;
                        ss.Photo2 = photoname2;
                        ss.Image1 = imagename1;
                        ss.Image2 = imagename2;

                        ss.NotificationId = notiid;
                        ss.InspectionId = nxtinspctid;
                        ss.RopeTail = 1;
                        ss.IsActive = true;
                                          //ss.InspectionId = NextInspectionId();

                                          ss.CreatedDate = DateTime.Now;
                                          ss.CreatedBy = "Admin";



                                          sc.MooringRopeInspectionTbl.Add(ss);

                        if (insdtcheck == 0)
                        {

                            int firstvalue = Convert.ToInt32(item.ExternalRating_A);
                            int secondvalue = Convert.ToInt32(item.InternalRating_A);
                            decimal finalvalue = (firstvalue + secondvalue);
                            finalvalue = finalvalue / 2;

                            finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                            item.AverageRating_A = Convert.ToInt32(finalvalue);

                            decimal avRA = item.AverageRating_A;

                            avg = avRA;


                            var RopeId = Ropelist.Where(x => x.Id == item.RopeId && x.RopeTail == 1).FirstOrDefault();

                            string isTail = RopeId.RopeTail == 1 ? "Tail" : string.Empty;

                            var certinumber = Ropelist.Where(x => x.Id == item.RopeId && x.OutofServiceDate == null && x.DeleteStatus == false && x.RopeTail == 1).Select(x => x.CertificateNumber + " - " + x.UniqueID).SingleOrDefault();


                                                 if (item.Chafe_guard_condition == "Not Acceptable")
                                                 {
                                                        if (imagename1 == null && imagename2 == null)
                                                        {
                                                               MainViewModelWorkHours.CommonValue = true;
                                                               System.Windows.MessageBox.Show("Due to the reported condition of Line ID {" + certinumber + "}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "Line Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                                               return;
                                                        }
                                                 }

                                                 if (avg >= 5 && avg < 6)
                            {
                                var Rating_5rope = "";
                                if (item.AssignNumber != null || item.AssignNumber != "")
                                {
                                    Rating_5rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                }
                                else
                                {
                                    Rating_5rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + "";
                                }
                                //var Rating_5rope = "Rating 5 - Rope #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;

                                if (imagename1 == null && imagename2 == null)
                                {
                                    MainViewModelWorkHours.CommonValue = true;
                                    System.Windows.MessageBox.Show("Due to the reported condition of Line ID {" + certinumber + "}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                   
                                    return;
                                }
                                else
                                {
                                    int NotiAlertType = (int)NotificationAlertType.Rating_5Rope;
                                    InspectNotification(item.RopeId, Rating_5rope, NotiAlertType, DateTime.Now.Date);
                                }

                              


                            }
                            else if (avg >= 6 && avg < 7)
                            {
                                var Rating_6rope = "";
                                if (item.AssignNumber != null || item.AssignNumber != "")
                                {
                                    Rating_6rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                }
                                else
                                {
                                    Rating_6rope = "Rating " + avg + " - Rope" + isTail + " #" + certinumber + "";
                                }
                                //var Rating_5rope = "Rating 5 - Rope #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;

                                if (imagename1 == null && imagename2 == null)
                                {
                                    MainViewModelWorkHours.CommonValue = true;
                                    System.Windows.MessageBox.Show("Due to the reported condition of Line ID {" + certinumber + "}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                                else
                                {
                                    int NotiAlertType = (int)NotificationAlertType.Rating_5Rope;
                                    InspectNotification(item.RopeId, Rating_6rope, NotiAlertType, DateTime.Now.Date);

                                }


                            }
                            else if (avg >= 7)
                            {

                                var Rating_7rope = "";
                                if (item.AssignNumber != null || item.AssignNumber != "")
                                {
                                    Rating_7rope = "Rating 7 - Rope" + isTail + " #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                }
                                else
                                {
                                    Rating_7rope = "Rating 7 - Rope" + isTail + " #" + certinumber + "";
                                }
                                // var Rating_7rope = "Rating 7 - Rope #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;


                                if (imagename1 == null && imagename2 == null)
                                {
                                    MainViewModelWorkHours.CommonValue = true;
                                    System.Windows.MessageBox.Show("Due to the reported condition of Line ID {" + certinumber + "}, it is compulsory to attach atleast 1 photograph where maximum abrasion/ damage is observed, please browse and attach photographs", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                                else
                                {
                                    int NotiAlertType = (int)NotificationAlertType.Rating_7Rope;
                                    InspectNotification3(item.RopeId, Rating_7rope, NotiAlertType, DateTime.Now.Date);

                                }


                            }

                            var ratingcheck = InspecSetting.Where(x => x.MooringRopeType == RopeId.RopeTypeId && x.ManufacturerType == RopeId.ManufacturerId).FirstOrDefault();
                            if (ratingcheck != null)
                            {
                                decimal[] array = new decimal[7] { ratingcheck.Rating1, ratingcheck.Rating2, ratingcheck.Rating3, ratingcheck.Rating4, ratingcheck.Rating5, ratingcheck.Rating6, ratingcheck.Rating7 };

                                var nearest = array.OrderBy(x => Math.Abs((long)x - avg)).First();
                                int near = Convert.ToInt32(nearest);

                                string rating = "Rating" + avg;


                                SqlDataAdapter pp = new SqlDataAdapter("select " + rating + " from tblRopeTailInspectionSetting where mooringropetype=" + RopeId.RopeTypeId + " and manufacturertype=" + RopeId.ManufacturerId + "", sc.con);
                                System.Data.DataTable rtc = new System.Data.DataTable();
                                pp.Fill(rtc);
                                if (rtc.Rows.Count > 0)
                                {
                                    decimal perchk = Convert.ToDecimal(rtc.Rows[0][0]) * 30 / 100;
                                    perchk = perchk * 100;
                                    //near = Convert.ToInt32(rtc.Rows[0][0]);
                                    near = Convert.ToInt32(perchk);
                                    // near = Convert.ToInt32(rtc.Rows[0][0]);
                                }

                                DateTime notidueMonth = Convert.ToDateTime(ss.InspectDate).AddDays(near);

                                var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == item.RopeId && b.IsActive == true && b.DeleteStatus == false && b.RopeTail == 1);
                                if (result != null)
                                {
                                    result.InspectionDueDate = notidueMonth;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    //sc.SaveChanges();
                                }



                                var notification = "";
                                if (item.AssignNumber != null && item.AssignNumber != "")
                                {
                                    notification = "Inspection Due on 7 days- RopeTail #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                }
                                else
                                {
                                    notification = "Inspection Due on 7 days- RopeTail #" + certinumber + "";
                                }
                                //var notification = "Inspection Due on 7 days- RopeTail #" + certinumber + " on Winch " + item.AssignNumber + " located at " + item.Location;
                                NotificationsClass noti = new NotificationsClass();
                                noti.Acknowledge = false;
                                noti.AckRecord = "Not yet acknowledged";
                                noti.Notification = notification;
                                noti.NotificationDueDate = notidueMonth;
                                noti.RopeId = item.RopeId;
                                noti.IsActive = true;
                                noti.CreatedDate = DateTime.Now;
                                noti.CreatedBy = "Admin";
                                sc.Notifications.Add(noti);

                                StaticHelper.AlarmFunction(1, true);
                            }
                        }
                    }
                }
            }
            if (insdtcheck == 0)
            {
                sc.SaveChanges();
                System.Windows.MessageBox.Show("Record saved successfully", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                CancelRopeInspection();
            }
            else
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("The inspection date is earlier than installed date for these ropetail " + test + " , Kindly change the inspection date or installed dates of these ropes from rope detail form", "RopeTail Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        //private void CancelRopeInspection()
        //{
        //    var lostdata = new ObservableCollection<MooringRopeInspectionClass>(sc.MooringRopeInspectionTbl.ToList());
        //    TailInspectionListViewModel cc = new TailInspectionListViewModel(lostdata);

        //    ChildWindowManager.Instance.CloseChildWindow();
        //}

        public int NextInspectionId()
        {
            int inspectionid = 0;
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("select max(inspectionid) from mooringropeinspection", sc.con);
                System.Data.DataTable dt = new System.Data.DataTable();
                adp.Fill(dt);
                inspectionid = Convert.ToInt32(dt.Rows[0][0]) + 1;

                return inspectionid;

            }
            catch { return inspectionid; }

        }

        public static ObservableCollection<MooringRopeInspectionClass> loadUserAccess = new ObservableCollection<MooringRopeInspectionClass>();
        public ObservableCollection<MooringRopeInspectionClass> LoadInspections
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
            }
        }

        private ObservableCollection<MooringRopeInspectionClass> GetMooringInspection(TotalInspections TotIns)
        {

            MooringTailInspectionView.imagesavelist.Clear();
            MooringTailInspectionView.imagesavelist1.Clear();
            DateTime dd = TotIns.InspectDate;
            MooringInspect.InspectDate = TotIns.InspectDate;
            MooringInspect.InspectBy = TotIns.InspectBy;
            int id = TotIns.id;

            //string qry = "select * from MooringRopeInspection where InspectDate = '" + dd.ToShortDateString() + "'";  // 2019-07-12 00:00:00.000 in sql tbl

            string qry = "EditRopeInspection";  // 2019-07-12 00:00:00.000 in sql tbl
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);

            sda.SelectCommand.Parameters.AddWithValue("@inspectionid", id);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            System.Data.DataTable datatbl = new System.Data.DataTable();


            sda.Fill(datatbl);

            var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

            for (int i = 0; i < datatbl.Rows.Count; i++)
            {
                PhotoList myphoto = new PhotoList()
                {
                    RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                    WinchId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),
                    // photo1 = ss,

                    photo1 = (datatbl.Rows[i]["Photo1"] == DBNull.Value) ? null : (byte[])datatbl.Rows[i]["Photo1"],
                    //photo1 = Convert.ToByte(datatbl.Rows[i]["RopeId"]),
                    imagename1 = datatbl.Rows[i]["Image1"].ToString() == null ? null : datatbl.Rows[i]["Image1"].ToString()

                };
                MooringTailInspectionView.imagesavelist.Add(myphoto);




                PhotoList1 myphoto1 = new PhotoList1()
                {
                    RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                    WinchId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),
                    // photo1 = ss,

                    //photo2 = (byte[])datatbl.Rows[i]["Photo2"],
                    //imagename2 = datatbl.Rows[i]["Image2"].ToString()

                    photo2 = (datatbl.Rows[i]["Photo2"] == DBNull.Value) ? null : (byte[])datatbl.Rows[i]["Photo2"],
                    //photo1 = Convert.ToByte(datatbl.Rows[i]["RopeId"]),
                    imagename2 = datatbl.Rows[i]["Image2"].ToString() == null ? null : datatbl.Rows[i]["Image2"].ToString()

                };
                MooringTailInspectionView.imagesavelist1.Add(myphoto1);



                ranklist.Add(new MooringRopeInspectionClass()
                {

                    Id = Convert.ToInt32(datatbl.Rows[i]["Id"]),
                    RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                    WinchId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),
                    //InspectBy = datatbl.Rows[i]["InspectBy"].ToString(),
                    InspectDate = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"]),
                    //AssignNumber = datatbl.Rows[i]["AssignedNumber"].ToString(),
                    //Location = datatbl.Rows[i]["Location"].ToString(),


                    AssignNumber = datatbl.Rows[i]["AssignedNumber"] == DBNull.Value ? "Not Assigned" : datatbl.Rows[i]["AssignedNumber"].ToString(),
                    Location = datatbl.Rows[i]["Location"] == DBNull.Value ? "Not Assigned" : datatbl.Rows[i]["Location"].ToString(),

                    RpoeType = datatbl.Rows[i]["RopeType"].ToString(),

                    Photo1 = myphoto.photo1,
                    Photo2 = myphoto1.photo2,
                    Image1 = myphoto.imagename1,
                    Image2 = myphoto1.imagename2,


                    Photo11 = datatbl.Rows[i]["Photo11"].ToString(),
                    Photo12 = datatbl.Rows[i]["Photo12"].ToString(),

                    Showbutton1 = datatbl.Rows[i]["Showbutton1"].ToString(),
                    Showbutton2 = datatbl.Rows[i]["Showbutton2"].ToString(),

                    Certi_No = datatbl.Rows[i]["CertificateNumber"].ToString(),
                    UniqueId = datatbl.Rows[i]["UniqueId"].ToString(),
                    ExternalRating_A = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_A"]),
                    InternalRating_A = Convert.ToInt32(datatbl.Rows[i]["InternalRating_A"]),
                    AverageRating_A = Convert.ToInt32(datatbl.Rows[i]["AverageRating_A"]),
                    //LengthOFAbrasion_A = Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_A"]),
                    //DistanceOutboard_A = Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_A"]),
                    //CutYarnCount_A = Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_A"]),


                    //LengthOFGlazing_A = Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_A"]),

                    //ExternalRating_B = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_B"]),
                    //InternalRating_B = Convert.ToInt32(datatbl.Rows[i]["InternalRating_B"]),
                    //AverageRating_B = Convert.ToInt32(datatbl.Rows[i]["AverageRating_B"]),
                    LengthOFAbrasion_A = datatbl.Rows[i]["LengthOFAbrasion_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_A"]),
                    DistanceOutboard_A = datatbl.Rows[i]["DistanceOutboard_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_A"]),
                    CutYarnCount_A = datatbl.Rows[i]["CutYarnCount_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_A"]),
                    LengthOFGlazing_A = datatbl.Rows[i]["LengthOFGlazing_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_A"]),

                    //InternalRating_B = datatbl.Rows[i]["InternalRating_B"] == DBNull.Value ? 0 : Convert.ToInt32(datatbl.Rows[i]["InternalRating_B"]),
                    //ExternalRating_B = datatbl.Rows[i]["ExternalRating_B"] == DBNull.Value ? 0 : Convert.ToInt32(datatbl.Rows[i]["ExternalRating_B"]),

                    //AverageRating_B = datatbl.Rows[i]["AverageRating_B"] == DBNull.Value ? 0 : Convert.ToInt32(datatbl.Rows[i]["AverageRating_B"]),


                    //LengthOFAbrasion_B = datatbl.Rows[i]["LengthOFAbrasion_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_B"]),
                    //DistanceOutboard_B = datatbl.Rows[i]["DistanceOutboard_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_B"]),
                    //CutYarnCount_B = datatbl.Rows[i]["CutYarnCount_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_B"]),
                    //LengthOFGlazing_B = datatbl.Rows[i]["LengthOFGlazing_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_B"]),
                    //LengthOFAbrasion_B = Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_B"]),
                    //DistanceOutboard_B = Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_B"]),
                    //CutYarnCount_B = Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_B"]),
                    //LengthOFGlazing_B = Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_B"]),

                    Chafe_guard_condition = datatbl.Rows[i]["Chafe_guard_condition"].ToString(),

                    Twist = Convert.ToInt32(datatbl.Rows[i]["Twist"]),
                    IsCheckedV = true,
                    InspectionId = Convert.ToInt32(datatbl.Rows[i]["InspectionId"])
                });

                AllSelected = true;
            }

            return ranklist;
        }

        // private ObservableCollection<MooringRopeInspectionClass> GetMooringInspection()
        public void SelectAllMooringInspection()
        {
            try
            {
                List<MooringRopeInspectionClass> list = new List<MooringRopeInspectionClass>();
                list = LoadInspections.ToList();
                list.ForEach(x => {

                    x.IsCheckedV = AllSelected;



                   
                });

                LoadInspections.Clear();
                sc.ObservableCollectionList(LoadInspections, list);

                RaisePropertyChanged("LoadInspections");



                //MooringRopeInspectionClass rptlins;
                //DateTime dd = DateTime.Now.Date;

                ////string qry = "select b.AssignedNumber,b.Location,c.RopeType,c.CertificateNumber from AssignRopeToWinch a join MooringWinchDetail  b on a.WinchId=b.Id join MooringRopeDetail c on c.Id = a.RopeId";
                //string qry = "RopeInspection";
                //SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                //sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                //System.Data.DataTable datatbl = new System.Data.DataTable();
                //sda.Fill(datatbl);

                //var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

                //foreach (DataRow row in datatbl.Rows)
                //{
                //    LoadInspections.Add(new MooringRopeInspectionClass()
                //    {
                //        InspectBy = "Amit Rana",

                //        InspectDate = dd,

                //        AssignNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                //        Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                //        RpoeType = (string)row["RopeType"],
                //        Certi_No = (string)row["CertificateNumber"],
                //        UniqueId = (string)row["UniqueId"],
                //        RopeId = (int)row["RopeId"],
                //        WinchId = Convert.ToInt32((row["WinchId"] == DBNull.Value) ? 0 : row["WinchId"]),
                //        ExternalRating_A = 0,
                //        InternalRating_A = 0,
                //        AverageRating_A = 0,
                //        LengthOFAbrasion_A = 0.00m,
                //        DistanceOutboard_A = 0.00m,
                //        CutYarnCount_A = 0.00m,
                //        LengthOFGlazing_A = 0.00m,
                //        IsCheckedV = true,

                //        Showbutton1 = "Visible",
                //        Showbutton2 = "Visible",
                //        Photo11 = "Hidden",
                //        Photo12 = "Hidden",

                //    });
                //}
                //OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));

                //for (int i = 0; i < datatbl.Rows.Count; i++)
                //{
                //    ranklist.Add(new MooringRopeInspectionClass()
                //    {

                //        InspectBy = "Amit Rana",
                //        InspectDate = dd,
                //        AssignNumber = datatbl.Rows[i]["AssignedNumber"].ToString(),
                //        Location = datatbl.Rows[i]["Location"].ToString(),
                //        RpoeType = datatbl.Rows[i]["RopeType"].ToString(),
                //        Certi_No = datatbl.Rows[i]["CertificateNumber"].ToString(),
                //        RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                //        WinchId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),

                //        ExternalRating_A = 0,
                //        InternalRating_A = 0,
                //        AverageRating_A = 0,
                //        LengthOFAbrasion_A = 0.00m,
                //        DistanceOutboard_A = 0.00m,
                //        CutYarnCount_A = 0.00m,
                //        LengthOFGlazing_A = 0.00m,


                //        Chafe_guard_condition = "AAA",

                //        Twist = 0
                //    });


                //}

                // return ranklist;
            }
            catch (Exception ex) { }
        }


        public void GetMooringInspection()
        {
            try
            {
                LoadInspections.Clear();
                MooringRopeInspectionClass rptlins;
                DateTime dd = DateTime.Now.Date;

                //string qry = "select b.AssignedNumber,b.Location,c.RopeType,c.CertificateNumber from AssignRopeToWinch a join MooringWinchDetail  b on a.WinchId=b.Id join MooringRopeDetail c on c.Id = a.RopeId";
                string qry = "RopeInspection";
                SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                System.Data.DataTable datatbl = new System.Data.DataTable();
                sda.Fill(datatbl);

                var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

                foreach (DataRow row in datatbl.Rows)
                {
                    LoadInspections.Add(new MooringRopeInspectionClass()
                    {
                        InspectBy = "Amit Rana",

                        InspectDate = dd,

                        AssignNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                        Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                        RpoeType = (string)row["RopeType"],
                        Certi_No = (string)row["CertificateNumber"],
                        RopeId = (int)row["RopeId"],
                        UniqueId = (string)row["UniqueId"],
                        WinchId = Convert.ToInt32((row["WinchId"] == DBNull.Value) ? 0 : row["WinchId"]),
                        ExternalRating_A = 0,
                        InternalRating_A = 0,
                        AverageRating_A = 0,
                        LengthOFAbrasion_A = 0.00m,
                        DistanceOutboard_A = 0.00m,
                        CutYarnCount_A = 0.00m,
                        LengthOFGlazing_A = 0.00m,
                        IsCheckedV=false,

                        Photo11 = "Hidden",
                        Photo12 = "Hidden",

                        Showbutton1 = "Visible",
                        Showbutton2 = "Visible",

                    });

                    AllSelected = false;
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));

                //for (int i = 0; i < datatbl.Rows.Count; i++)
                //{
                //    ranklist.Add(new MooringRopeInspectionClass()
                //    {

                //        InspectBy = "Amit Rana",
                //        InspectDate = dd,
                //        AssignNumber = datatbl.Rows[i]["AssignedNumber"].ToString(),
                //        Location = datatbl.Rows[i]["Location"].ToString(),
                //        RpoeType = datatbl.Rows[i]["RopeType"].ToString(),
                //        Certi_No = datatbl.Rows[i]["CertificateNumber"].ToString(),
                //        RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                //        WinchId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),

                //        ExternalRating_A = 0,
                //        InternalRating_A = 0,
                //        AverageRating_A = 0,
                //        LengthOFAbrasion_A = 0.00m,
                //        DistanceOutboard_A = 0.00m,
                //        CutYarnCount_A = 0.00m,
                //        LengthOFGlazing_A = 0.00m,


                //        Chafe_guard_condition = "AAA",

                //        Twist = 0
                //    });


                //}

                // return ranklist;
            }
            catch (Exception ex) { }
        }

        //public ObservableCollection<string> ComboItems { get; set; }
        //private string comboValue;
        //public string ComboValue
        //{
        //       get { return comboValue; }
        //       set
        //       {
        //              if (comboValue != value)
        //              {
        //                     comboValue = value;
        //                     OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
        //              }
        //       }
        //}


        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        public static MooringRopeInspectionClass _mooringInspact = new MooringRopeInspectionClass();

        public MooringRopeInspectionClass MooringInspect
        {
            get { return _mooringInspact; }
            set
            {
                _mooringInspact = value;
                RaisePropertyChanged("MooringInspect");
            }
        }


        public static Nullable<DateTime> _EndtoEndDoneDate = null;
        public Nullable<DateTime> EndtoEndDoneDate
        {
            get
            {
                if (_EndtoEndDoneDate == null)
                {
                    _EndtoEndDoneDate = Convert.ToDateTime(DateTime.Now);
                }
                _mooringInspact.InspectDate = (DateTime)_EndtoEndDoneDate;
                return _EndtoEndDoneDate;
            }
            set
            {
                _EndtoEndDoneDate = value;
                RaisePropertyChanged("EndtoEndDoneDate");
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
