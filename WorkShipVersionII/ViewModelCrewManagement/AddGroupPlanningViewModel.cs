using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddGroupPlanningViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;
        private int erinfo;
        public AddGroupPlanningViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            cancelCommand = new RelayCommand(CancelGroup);
            saveCommand = new RelayCommand<AddGroupClass>(SaveGroup);
            nextCommand = new RelayCommand<string>(NextButton);
            backCommand = new RelayCommand<string>(BackButton);
            deptMemberList.Clear();
            deptMemberList = GetDeptCrew();

        }

        public AddGroupPlanningViewModel(AddGroupClass obj)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            cancelCommand = new RelayCommand(CancelGroup);
            saveCommand = new RelayCommand<AddGroupClass>(UpdateMethod);
            nextCommand = new RelayCommand<string>(NextButton);
            backCommand = new RelayCommand<string>(BackButton);
            deptMemberList.Clear();
            deptMemberList = GetDeptCrew();

            deptMemberList1.Clear();
            deptMemberList1 = GetDeptCrew(obj);
            RaisePropertyChanged("DeptMemberList1");
            AddGroup.GroupName = obj.GroupName;
            AddGroup.GroupID = obj.GroupID;
            RaisePropertyChanged("AddGroup");

        }





        private static AddGroupClass _AddGroup = new AddGroupClass();
        public AddGroupClass AddGroup
        {
            get
            {
                if (erinfo != 0)
                {
                    refreshmessage(_AddGroup);
                    RaisePropertyChanged("AddGroupMessage");
                }
                return _AddGroup;
            }
            set
            {
                _AddGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddGroup"));
            }
        }


        private static AddGroupErrorMessages _AddGroupMessage = new AddGroupErrorMessages();
        public AddGroupErrorMessages AddGroupMessage
        {
            get
            {
                return _AddGroupMessage;
            }
            set
            {
                _AddGroupMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddGroupMessage"));
            }
        }

        private string s_DeptMemberList;
        public string S_DeptMemberList
        {
            get { return s_DeptMemberList; }
            set
            {
                s_DeptMemberList = value;
                RaisePropertyChanged("S_DeptMemberList");
            }
        }

        private string s_DeptMemberList1;
        public string S_DeptMemberList1
        {
            get { return s_DeptMemberList1; }
            set
            {
                s_DeptMemberList1 = value;
                RaisePropertyChanged("S_DeptMemberList1");
            }
        }

        private static ObservableCollection<string> deptMemberList = new ObservableCollection<string>();
        public ObservableCollection<string> DeptMemberList
        {
            get { return deptMemberList; }
            set
            {
                deptMemberList = value;
                RaisePropertyChanged("DeptMemberList");
            }
        }
        private static ObservableCollection<string> deptMemberList1 = new ObservableCollection<string>();
        public ObservableCollection<string> DeptMemberList1
        {
            get
            {

                return deptMemberList1;
            }
            set
            {
                deptMemberList1 = value;
                RaisePropertyChanged("DeptMemberList1");
            }
        }


        private ObservableCollection<string> GetDeptCrew()
        {

            var CrewNames = new ObservableCollection<string>();

            DateTime dd = DateTime.Now.Date;

            var data = sc.CrewDetails.Where(p => p.ServiceTo >= dd).Select(x => new { x.name, x.position }).ToList();


            foreach (var item in data)
            {
                CrewNames.Add(item.position + " - " + item.name);

            }


            return CrewNames;
        }

        private ObservableCollection<string> GetDeptCrew(AddGroupClass obj)
        {

            var CrewNames = new ObservableCollection<string>();

            DateTime dd = DateTime.Now.Date;

            var data = obj.GroupMember.Split(',');


            foreach (var item in data)
            {
                CrewNames.Add(item);

            }


            return CrewNames;
        }

        private void refreshmessage(AddGroupClass cdc)
        {

            CheckErrorMessage.CheckErrorMessages = true;

            AddGroupErrorMessages m = (AddGroupMessage as AddGroupErrorMessages); //DownCasting.....

            if (!string.IsNullOrEmpty(cdc.GroupName))
            {
                CheckErrorMessage.CheckErrorMessages = true;
                m.GroupNameMessage = string.Empty;
                RaisePropertyChanged("AddGroupMessage");
            }
            else
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.GroupNameMessage = "Please enter the Group Name";
                RaisePropertyChanged("AddGroupMessage");
            }
            if (deptMemberList1.Count() > 0)
            {
                CheckErrorMessage.CheckErrorMessages = true;
                m.GroupMemberMessage = string.Empty;
                RaisePropertyChanged("AddGroupMessage");
            }
            else
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.GroupMemberMessage = "Please add member into Group";
                RaisePropertyChanged("AddGroupMessage");
            }

        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
        }

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return nextCommand;
            }

        }

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get
            {
                return backCommand;
            }

        }

        private void NextButton(string obj)
        {

            if (!string.IsNullOrEmpty(obj))
            {
                var bb = DeptMemberList1.Where(x => x.Equals(obj)).FirstOrDefault();
                if (bb == null)
                    DeptMemberList1.Add(obj);
                else
                    MessageBox.Show("This crew member is already present in the Group");
            }

            RaisePropertyChanged("AddGroup");


        }
        private void BackButton(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {

                DeptMemberList1.RemoveAt(DeptMemberList1.IndexOf(obj));
            }
            RaisePropertyChanged("AddGroup");

        }
        private void SaveGroup(AddGroupClass obj)
        {
            erinfo = 1;
            refreshmessage(obj);
            if (!string.IsNullOrEmpty(AddGroup.GroupName) && deptMemberList1.Count() > 0)
            {

                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {

                    var position = new List<string>();
                    var name = new List<string>();
                    var username = new List<string>();

                    foreach (var item in DeptMemberList1)
                    {
                        var data = sc.CrewDetails.Select(x => new { member = x.position + " - " + x.name, x.position, x.name, x.UserName }).Where(p => p.member == item).ToList();

                        foreach (var s in data)
                        {
                            position.Add(s.position);
                            name.Add(s.name);
                            username.Add(s.UserName);
                        }

                    }
                    obj.GroupMember = string.Join(",", DeptMemberList1);
                    obj.GRank = string.Join(",", position);
                    obj.GFullName = string.Join(",", name);
                    obj.GUser = string.Join(",", username);


                    sc1.AddGroups.Add(obj);
                    sc.SaveChanges();


                    MessageBox.Show("Group Plan Added", "Group Plan");
                    Refreshmethod();
                    GetAddGroupDetail();



                }

            }
        }

        private void UpdateMethod(AddGroupClass obj)
        {

            erinfo = 1;
            refreshmessage(obj);
            if (!string.IsNullOrEmpty(AddGroup.GroupName) && deptMemberList1.Count() > 0)
            {


                var position = new List<string>();
                var name = new List<string>();
                var username = new List<string>();

                foreach (var item in DeptMemberList1)
                {
                    var data = sc.CrewDetails.Select(x => new { member = x.position + " - " + x.name, x.position, x.name, x.UserName }).Where(p => p.member == item).ToList();

                    foreach (var s in data)
                    {
                        position.Add(s.position);
                        name.Add(s.name);
                        username.Add(s.UserName);
                    }

                }
                obj.GroupMember = string.Join(",", DeptMemberList1);
                obj.GRank = string.Join(",", position);
                obj.GFullName = string.Join(",", name);
                obj.GUser = string.Join(",", username);

                sc.Entry(obj).State = EntityState.Modified;
                sc.SaveChanges();

                MessageBox.Show("Group Plan Updated", "Group Plan");
                GetAddGroupDetail();
                CancelGroup();

            }

        }


        private void GetAddGroupDetail()
        {
            GroupPlanningViewModel.loadAddGroup.Clear();
            var data = sc.AddGroups.Select(x => new { x.GroupID, x.GroupName, x.GroupMember, x.GFullName, x.GRank, x.GUser }).ToList();

            foreach (var item in data)
            {
                GroupPlanningViewModel.loadAddGroup.Add(new AddGroupClass()
                {
                    GroupID = item.GroupID,
                    GroupName = item.GroupName,
                    GroupMember = item.GroupMember,
                    GFullName = item.GFullName,
                    GRank = item.GRank,
                    GUser = item.GUser
                });
            }

        }
        private void Refreshmethod()
        {
            erinfo = 0;
            CheckErrorMessage.CheckErrorMessages = false;

            AddGroupMessage = new AddGroupErrorMessages();
            RaisePropertyChanged("AddGroupMessage");
            AddGroup = new AddGroupClass();
            RaisePropertyChanged("AddGroup");
            DeptMemberList1.Clear();
            RaisePropertyChanged("DeptMemberList1");




        }

        private void CancelGroup()
        {
            Refreshmethod();
            ChildWindowManager.Instance.CloseChildWindow();

        }


        new private event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
