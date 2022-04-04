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

namespace WorkShipVersionII.WorkHoursViewModel
{ 
    public class AddMooringWinchDetailsViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddMooringWinchDetailsViewModel(MooringWinchClass edeps)
        {
            StaticHelper.Editing = true;
            isvisible = false;
            RaisePropertyChanged("ISvisible");
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            GetLead();
            saveCommand = new RelayCommand<MooringWinchClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            EditMooringWinch(edeps);
        }

        private bool isvisible = true;

        public bool ISvisible
        {
            get
            {
                return isvisible;
            }
            set
            { isvisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ISvisible"));
            }
        }

        public AddMooringWinchDetailsViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            GetLead();
            saveCommand = new RelayCommand<MooringWinchClass>(SaveMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);
        }

        public class LeadCombo
        {
            public int Id { get; set; }
            public string Lead { get; set; }
        }
        public static ObservableCollection<LeadCombo> lead = new ObservableCollection<LeadCombo>();
        public ObservableCollection<LeadCombo> Lead
        {
            get
            {
                return lead;
            }
            set
            {
                lead = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Lead"));
            }
        }

        public static LeadCombo slead = new LeadCombo();
        public LeadCombo SLead
        {          
            get
            {
                if (slead != null)
                {
                    _AddMooringWinch.Lead = slead.Lead;
                    OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinch"));
                }
                return slead;
            }
            set
            {
                slead = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SLead"));
            }
        }
        public void GetLead()
        {
            try
            {
                lead.Clear();
                SqlCommand cmd = new SqlCommand("select ID, Name as Lead from tblCommon where TYPE = 6", sc.con);
                //cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    var leads = (string)row["Lead"];
                    leads = leads.Replace(Environment.NewLine, "").Trim();
                    lead.Add(new LeadCombo()
                    {
                        Id = (int)row["Id"],
                       
                        Lead = leads,

                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("lead"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }
        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        public static MooringWinchClass _AddMooringWinch = new MooringWinchClass();
        public MooringWinchClass AddMooringWinch
        {
            get
            {
                MooringWinchMessage = string.Empty;
                RaisePropertyChanged("MooringWinchMessage");
                return _AddMooringWinch;
            }
            set
            {
                _AddMooringWinch = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinch"));
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

        private void SaveMooringWinch(MooringWinchClass moorwinch)
        {
            try
            {
                moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
                {
                    var AllWincheslist = sc.MooringWinch.ToList();
                    AllWincheslist.ForEach(x => {
                        x.AssignedNumber = x.AssignedNumber.Replace(Environment.NewLine, "").Trim();
                    });
                    var findassno = AllWincheslist.Where(x => x.AssignedNumber == moorwinch.AssignedNumber && x.IsActive==true).FirstOrDefault();

                    if (findassno == null)
                    {
                        moorwinch.AssignedNumber = moorwinch.AssignedNumber;
                        if(moorwinch.Location==null)
                        {
                            MessageBox.Show("Please Enter Assigned Location", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (moorwinch.MBL == null)
                        {
                            MessageBox.Show("Please Enter MBHC", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (moorwinch.Lead == null || moorwinch.Lead== "--Select--")
                        {
                            MessageBox.Show("Please select lead !", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        //moorwinch.Location = textinfo.ToTitleCase(moorwinch.Location.ToLower());

                        var sortingorder = sc.MooringWinch.Where(x=>x.IsActive==true).DefaultIfEmpty().Max(r => r == null ? 1 : r.SortingOrder) + 1;

                        moorwinch.SortingOrder = sortingorder;
                        moorwinch.Location = moorwinch.Location;
                        moorwinch.MBL = Convert.ToDecimal(moorwinch.MBL);
                        moorwinch.CreatedDate = DateTime.Now;
                        moorwinch.CreatedBy = "Admin";
                        moorwinch.IsActive = true;

                        sc.MooringWinch.Add(moorwinch);
                        sc.SaveChanges();

                      
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelMooringWinch();

                        AddMooringWinch = new MooringWinchClass();
                        RaisePropertyChanged("AddMooringWinch");


                    }
                    else
                    {
                        MessageBox.Show("MooringWinch already exist ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Ship Assigned Number", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);

                    MooringWinchMessage = "Please Enter the MooringWinch Name";
                    RaisePropertyChanged("MooringWinchMessage");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

      

        private void UpdateMooringWinch(MooringWinchClass moorwinch)
        {
            try
            {
               
                moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
                {

                    var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id && x.IsActive == true).FirstOrDefault();

                    if (findrank != null)
                    {
                        //var AllWincheslist = sc.MooringWinch.ToList();
                        //AllWincheslist.ForEach(x =>
                        //{
                        //    x.AssignedNumber = x.AssignedNumber.Replace(Environment.NewLine, "").Trim();
                        //});
                        //var findassno = AllWincheslist.Where(x => x.AssignedNumber == moorwinch.AssignedNumber && x.IsActive == true).FirstOrDefault();

                        //if (findassno != null)
                        //{
                        //    MessageBox.Show("MooringWinch already exist ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                        //    return;
                        //}
                        //else
                        //{


                            moorwinch.AssignedNumber = moorwinch.AssignedNumber;



                            var local = sc.Set<MooringWinchClass>()
                             .Local
                             .FirstOrDefault(f => f.Id == moorwinch.Id);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }

                            if (moorwinch.Location == null || moorwinch.Location == "")
                            {
                                MessageBox.Show("Please Enter Assigned Location", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            if (moorwinch.MBL == null || moorwinch.MBL == 0)
                            {
                                MessageBox.Show("Please Enter MBHC", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }


                        SqlDataAdapter adp = new SqlDataAdapter("update mooringwinchdetail set MBL=@MBL, AssignedNumber= @AssignedNumber , Location =@Location, Lead=@Lead where ID= " + moorwinch.Id + "", sc.con);
                       // SqlDataAdapter adp = new SqlDataAdapter("Updatetmooringwinchdetail", sc.con);
                        //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", moorwinch.Id);
                        adp.SelectCommand.Parameters.AddWithValue("@MBL", moorwinch.MBL);
                        adp.SelectCommand.Parameters.AddWithValue("@AssignedNumber", moorwinch.AssignedNumber);
                        adp.SelectCommand.Parameters.AddWithValue("@Location", moorwinch.Location);
                        adp.SelectCommand.Parameters.AddWithValue("@Lead", moorwinch.Lead);
                        DataTable dt = new DataTable();
                            adp.Fill(dt);
                            ISvisible = true;
                            RaisePropertyChanged("ISVisible");
                            //var UpdatedLocation = new MooringWinchClass()
                            //{

                            //    Id = moorwinch.Id,
                            //    AssignedNumber = moorwinch.AssignedNumber,
                            //    Location = moorwinch.Location,
                            //    MBL=moorwinch.MBL,
                            //    SortingOrder=moorwinch.SortingOrder,
                            //    Lead=moorwinch.Lead,
                            //    CreatedDate = DateTime.Now,
                            //    CreatedBy = "Admin",
                            //    ModifiedDate = DateTime.Now,
                            //    IsActive=true
                            //};

                            //sc.Entry(UpdatedLocation).State = EntityState.Modified;
                            //sc.SaveChanges();


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


                            CancelMooringWinch();

                        }
                    //}

                }
                else
                {
                    MessageBox.Show("Please Enter Ship Assigned Number", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);

                    MooringWinchMessage = "Please Enter the MooringWinch Name";
                    RaisePropertyChanged("MooringWinchMessage");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void EditMooringWinch(MooringWinchClass moorwinch)
        {
            try
            {
                //LeadCombo kkk = new LeadCombo();
                var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id && x.IsActive == true).FirstOrDefault();
                AddMooringWinch.AssignedNumber = findrank.AssignedNumber;
                AddMooringWinch.Location = findrank.Location;
                AddMooringWinch.MBL = findrank.MBL;
                //AddMooringWinch.SortingOrder = findrank.SortingOrder;
                AddMooringWinch.Lead = findrank.Lead.Trim();
                //kkk.Lead = findrank.Lead;
                //kkk.Id= findrank.Id;
                //SLead = kkk;
                AddMooringWinch.Id = findrank.Id;
               // AddMooringWinch.ModifiedDate = findrank.ModifiedDate;
                OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinch"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelMooringWinch()
        {
            var lostdata = new ObservableCollection<MooringWinchClass>(sc.MooringWinch.Where(x=> x.IsActive==true).ToList());
            MooringWinchViewModel cc = new MooringWinchViewModel(lostdata);

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
