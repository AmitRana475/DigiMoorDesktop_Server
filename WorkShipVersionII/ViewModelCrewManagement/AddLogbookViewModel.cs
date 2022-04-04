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
using System.Windows.Controls;
using System.Windows.Input;
using WorkShipVersionII.Core;
using WorkShipVersionII.ViewModel;


namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddLogbookViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private int erinfo = 0;
        int sa = 0;
        //Grid DynamicGrid;
        public AddLogbookViewModel(LogbookClass obj)
        {
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            //if (DynamicGrid == null)
            //    DynamicGrid = new Grid();

            addCommand = new RelayCommand<LogStatus>(AddMethod);
            removeCommand = new RelayCommand<LogStatus>(RemoveMethod);
            saveCommand = new RelayCommand<LogbookClass>(UpdateMethod);
            cacelCommand = new RelayCommand(CancelMethod);
            statusCommand = new RelayCommand<LogbookClass>(CheckStatusMethod);
            EditMethod(obj);


        }



        public AddLogbookViewModel()
        {
            DateHelper bm = new DateHelper();

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            //if (DynamicGrid == null)
            //    DynamicGrid = new Grid();

            saveCommand = new RelayCommand<LogbookClass>(SaveMethod);
            cacelCommand = new RelayCommand(CancelMethod);
            statusCommand = new RelayCommand<LogbookClass>(CheckStatusMethod);
            addCommand = new RelayCommand<LogStatus>(AddMethod);
            removeCommand = new RelayCommand<LogStatus>(RemoveMethod);
            GetRankList(DateTime.Now.Date);

            //yearname = sc.GetYear();
            //monthname = sc.GetMonth();
            //yearnameTo = sc.GetYear();
            //monthnameTo = sc.GetMonth();

        }

        private void RemoveMethod(LogStatus obj)
        {
            //peopleList.Where(x => x.UserName == obj.UserName).ToList().ForEach(x => x.IsChecked = false);
            //var list = peopleList.ToList();
            //peopleList.Clear();
            //list.ForEach(peopleList.Add);
            //RaisePropertyChanged("PeopleList");

            //peopleList.ToList().ForEach(x => { x.DateFroms = Convert.ToDateTime(DateFroms); x.DateTo = Convert.ToDateTime(DateTos); x.Timefrom = x.Timefrom == null ? TimeFrom : x.Timefrom; x.Timeto = x.Timeto == null ? TimeTo : x.Timeto; });

            addPeopleList.Remove(obj);
            var addlist = addPeopleList.ToList();
            addPeopleList.Clear();
            addlist.ToList().ForEach(addPeopleList.Add);
            OnPropertyChanged(new PropertyChangedEventArgs("AddPeopleList"));


        }


        private string currentlySelectedDate;
        public string CurrentlySelectedDate
        {
            get
            {
                return currentlySelectedDate;
            }
            set
            {
                currentlySelectedDate = value;
                RaisePropertyChanged("CurrentlySelectedDate");

            }
        }




        private void GetRankList(DateTime? DateFromss)
        {
            try
            {
                DateTime dd = DateFromss == null ? DateTime.Now.Date : Convert.ToDateTime(DateFromss).Date;
                peopleList.Clear();
                List<LogStatus> list = sc.CrewDetails.Where(p => p.ServiceTo >= dd).Select(x => new LogStatus { Position = x.position, Position1 = x.position + "(" + x.UserName + ")", UserName = x.UserName, IsChecked = false }).ToList();



                //finalList.ToList().ForEach(x => x.Id = counter++);
                sc.ObservableCollectionList(peopleList, list.ToList());

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void CheckStatusMethod(LogbookClass obj1)
        {

            try
            {

                //var bb2 = AddPeopleList.Select(p => p.UserName).ToList();
                //obj1.UserName = bb2 != null ? string.Join(",", bb2) : string.Empty;
                //obj1.UserName = obj1.UserName.TrimEnd(',');

                string checkStatus = string.Empty;

                foreach (LogStatus obj in AddPeopleList)
                {
                    List<CheckDatelist> allDatesandtime = GetDateTimeMethod(obj);


                    foreach (var dt in allDatesandtime)
                    {
                        var data = WorkHoursList.Where(x => x.UserName == obj.UserName && x.dates == dt.Dates).ToList();

                        if (data.Count == 0)
                        {
                            checkStatus += obj.UserName + ",";
                        }
                        else
                        {
                            foreach (var user in data)
                            {
                                var s2 = user.hrs.TrimEnd(',').Split(',');

                                var skip = Convert.ToInt32(dt.Timefrom);
                                var take = Convert.ToInt32(dt.Timeto) - Convert.ToInt32(dt.Timefrom);

                                s2 = s2.Skip(skip).Select(i => i.ToString()).ToArray();
                                s2 = s2.Take(take).Select(i => i.ToString()).ToArray();

                                bool exists2 = s2.Contains("0");

                                if (exists2)
                                {
                                    checkStatus += obj.UserName + ",";
                                }
                            }
                        }


                    }

                }



                //List<CheckDatelist> allDatesandtime = GetDateTimeMethod(obj);
                //string checkStatus = string.Empty;

                //var username = obj1.UserName.Split(',');
                //foreach (var item in username)
                //{
                //foreach (var dt in allDatesandtime)
                //{
                //    var data = WorkHoursList.Where(x => x.UserName == item && x.dates == dt.Dates).ToList();

                //    if (data.Count == 0)
                //    {
                //        checkStatus += item + ",";
                //    }
                //    else
                //    {
                //        foreach (var user in data)
                //        {
                //            var s2 = user.hrs.TrimEnd(',').Split(',');

                //            var skip = Convert.ToInt32(dt.Timefrom);
                //            var take = Convert.ToInt32(dt.Timeto) - Convert.ToInt32(dt.Timefrom);

                //            s2 = s2.Skip(skip).Select(i => i.ToString()).ToArray();
                //            s2 = s2.Take(take).Select(i => i.ToString()).ToArray();

                //            bool exists2 = s2.Contains("0");

                //            if (exists2)
                //            {
                //                checkStatus += item + ",";
                //            }
                //        }
                //    }


                //}

                //}

                var check = checkStatus.TrimEnd(',').Split(',');
                if (!string.IsNullOrEmpty(string.Join(",", check)))
                {
                    var users = sc.CrewDetails.Where(x => check.Contains(x.UserName)).Distinct().ToList();

                    string status = string.Empty;
                    foreach (var item in check)
                    {
                        var tm = users.Where(x => x.UserName == item).FirstOrDefault();
                        status += tm.position + "(" + tm.UserName + ")" + ",";
                    }

                    string[] bbs = status.TrimEnd(',').Split(',');
                    bbs = bbs.Distinct().ToArray();

                    obj1.Status = string.Join(",", bbs);


                    MessageBox.Show("Not Match..." + "\n" + obj1.Status);
                }
                else
                {
                    MessageBox.Show("Please check the checkbox");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }





        //private bool allSelected;
        //public bool AllSelected
        //{
        //    get
        //    {
        //        return allSelected;
        //    }
        //    set
        //    {
        //        try
        //        {
        //            // peopleList.Clear();
        //            allSelected = value;
        //            peopleList.ToList().ForEach(x => { x.IsChecked = value; x.DateFroms = Convert.ToDateTime(DateFroms); x.DateTo = Convert.ToDateTime(DateTos); x.Timefrom = TimeFrom; x.Timeto = TimeTo; });


        //            var addlist = peopleList.Where(s => s.IsChecked).ToList();
        //            addPeopleList.Clear();
        //            addlist.ToList().ForEach(addPeopleList.Add);
        //            OnPropertyChanged(new PropertyChangedEventArgs("AddPeopleList"));


        //            List<LogStatus> bb = peopleList.ToList();
        //            peopleList.Clear();
        //            bb.ToList().ForEach(peopleList.Add);
        //            OnPropertyChanged(new PropertyChangedEventArgs("PeopleList"));

        //            if (erinfo == 1)
        //            {
        //                var bbb = peopleList.Where(s => s.IsChecked).Select(p => p.Position).ToList();
        //                logbookStatus.CrewGroup = bbb != null ? string.Join(",", bbb) : string.Empty;
        //                logbookStatus.CrewGroup = logbookStatus.CrewGroup.TrimEnd(',');

        //                refreshmessage(logbookStatus);
        //                refreshmessage1(logbookStatus);
        //            }

        //            RaisePropertyChanged("AllSelected");
        //        }
        //        catch (Exception ex)
        //        {
        //            sc.ErrorLog(ex);
        //            allSelected = value;
        //        }

        //    }
        //}


        private LogStatus sPeopleList;
        public LogStatus SPeopleList
        {
            get { return sPeopleList; }
            set
            {
                sPeopleList = value;
                RaisePropertyChanged("SPeopleList");
            }
        }

        private List<WorkHoursClass> _WorkHoursList;
        public List<WorkHoursClass> WorkHoursList
        {
            get
            {
                if (_WorkHoursList == null)
                {

                    _WorkHoursList = sc.WorkHourss.ToList();

                }

                return _WorkHoursList;
            }
            set
            {
                _WorkHoursList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
            }
        }

        private static ObservableCollection<LogStatus> peopleList = new ObservableCollection<LogStatus>();
        public ObservableCollection<LogStatus> PeopleList
        {
            get
            {
                return peopleList;
            }
            set
            {
                peopleList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PeopleList"));


            }
        }


        private static PassErrorMessages _AddGroupMessage = new PassErrorMessages();
        public PassErrorMessages AddGroupMessage
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


        private void refreshmessage(LogbookClass cdc)
        {

            CheckErrorMessage.CheckErrorMessages = false;
            PassErrorMessages m = (AddGroupMessage as PassErrorMessages); //DownCasting.....

            if (!string.IsNullOrEmpty(cdc.EventName))
            {
                CheckErrorMessage.CheckErrorMessages = true;
                m.FullNameMessage = string.Empty;
                RaisePropertyChanged("AddGroupMessage");
            }

            //if (!string.IsNullOrEmpty(cdc.CrewGroup))
            //{
            //    CheckErrorMessage.CheckErrorMessages = true;
            //    m.UserNameMessage = string.Empty;
            //    RaisePropertyChanged("AddGroupMessage");
            //}



        }

        private void refreshmessage1(LogbookClass cdc)
        {

            CheckErrorMessage.CheckErrorMessages = true;

            PassErrorMessages m = (AddGroupMessage as PassErrorMessages); //DownCasting.....

            if (string.IsNullOrEmpty(cdc.EventName))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.FullNameMessage = "Please Enter Event Name";
                RaisePropertyChanged("AddGroupMessage");
            }

            //if (string.IsNullOrEmpty(cdc.CrewGroup))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.UserNameMessage = "Please Select the CrewGroup";
            //    RaisePropertyChanged("AddGroupMessage");
            //}



        }


        private void EditMethod(LogbookClass obj)
        {
            try
            {
                logbookStatus = obj;
                timefrom = obj.DateFrom.ToString("HH:mm");
                RaisePropertyChanged("Timefrom");
                timeto = obj.DateTo.ToString("HH:mm");
                RaisePropertyChanged("Timeto");
                DateFroms = obj.DateFrom;
                RaisePropertyChanged("DateFroms");
                DateTos = obj.DateTo;
                RaisePropertyChanged("DateTos");
                OnPropertyChanged(new PropertyChangedEventArgs("LogbookStatus"));

                GetRankList(obj.DateFrom);

                string[] grop = obj.CrewGroup1.Split(',');

                foreach (var item in grop)
                {
                    peopleList.Where(s => s.Position1 == item).ToList().ForEach(x => x.IsChecked = true);
                }

                List<LogStatus> bb = peopleList.ToList();
                peopleList.Clear();
                bb.ToList().ForEach(peopleList.Add);
                OnPropertyChanged(new PropertyChangedEventArgs("PeopleList"));


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void UpdateMethod(LogbookClass obj)
        {
            try
            {
                erinfo = 1;

                LogStatus obj1 = new LogStatus();

                var bb = PeopleList.Where(s => s.IsChecked).Select(p => p.Position).ToList();
                obj.CrewGroup = bb != null ? string.Join(",", bb) : string.Empty;
                obj.CrewGroup = obj.CrewGroup.TrimEnd(',');

                var bb1 = PeopleList.Where(s => s.IsChecked).Select(p => p.Position1).ToList();
                obj.CrewGroup1 = bb1 != null ? string.Join(",", bb1) : string.Empty;
                obj.CrewGroup1 = obj.CrewGroup1.TrimEnd(',');

                var bb2 = PeopleList.Where(s => s.IsChecked).Select(p => p.UserName).ToList();
                obj.UserName = bb2 != null ? string.Join(",", bb2) : string.Empty;
                obj.UserName = obj.UserName.TrimEnd(',');


                refreshmessage(logbookStatus);
                refreshmessage1(logbookStatus);

                if (CheckErrorMessage.CheckErrorMessages)
                {
                    List<CheckDatelist> allDatesandtime = GetDateTimeMethod(obj1);
                    string checkStatus = string.Empty;

                    var username = obj.UserName.Split(',');
                    foreach (var item in username)
                    {
                        foreach (var dt in allDatesandtime)
                        {


                            var data = WorkHoursList.Where(x => x.UserName == item && x.dates == dt.Dates).ToList();

                            if (data.Count == 0)
                            {
                                checkStatus += item + ",";
                            }
                            else
                            {
                                foreach (var user in data)
                                {
                                    var s2 = user.hrs.TrimEnd(',').Split(',');

                                    var skip = Convert.ToInt32(dt.Timefrom);
                                    var take = Convert.ToInt32(dt.Timeto) - Convert.ToInt32(dt.Timefrom);

                                    s2 = s2.Skip(skip).Select(i => i.ToString()).ToArray();
                                    s2 = s2.Take(take).Select(i => i.ToString()).ToArray();

                                    bool exists2 = s2.Contains("0");

                                    if (exists2)
                                    {
                                        checkStatus += item + ",";
                                    }
                                }
                            }


                        }

                    }

                    var check = checkStatus.TrimEnd(',').Split(',');
                    if (!string.IsNullOrEmpty(string.Join(",", check)))
                    {
                        var users = sc.CrewDetails.Where(x => check.Contains(x.UserName)).Distinct().ToList();

                        string status = string.Empty;
                        foreach (var item in check)
                        {
                            var tm = users.Where(x => x.UserName == item).FirstOrDefault();
                            status += tm.position + "(" + tm.UserName + ")" + ",";
                        }

                        string[] bbs = status.TrimEnd(',').Split(',');
                        bbs = bbs.Distinct().ToArray();

                        obj.Status = string.Join(",", bbs);


                        sc.Entry(obj).State = EntityState.Modified;
                        sc.SaveChanges();



                        MessageBox.Show("Record updated successfully", "Update LogBook");


                        erinfo = 0;
                        //AllSelected = false;
                        //RaisePropertyChanged("AllSelected");

                        logbookStatus = new LogbookClass();
                        RaisePropertyChanged("LogbookStatus");
                        CheckErrorMessage.CheckErrorMessages = false;

                        CancelMethod();
                    }

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private List<CheckDatelist> GetDateTimeMethod(LogStatus obj)
        {
            List<DateTime> allDates = new List<DateTime>();
            List<CheckDatelist> allDatesandtime = new List<CheckDatelist>();


            for (DateTime date = obj.DateFroms; date <= obj.DateTo; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            if (allDates.Count > 1)
            {
                for (int i = 0; i < allDates.Count; i++)
                {
                    if (i == 0)
                    {

                        //var timefrom = obj.DateFroms.ToString("HH:mm");
                        var timefrom = obj.Timefrom.ToString();
                        timefrom = timefrom.Replace(":", ".");
                        int from = Convert.ToInt32(Convert.ToDecimal(timefrom) * 2);

                        var dt = new CheckDatelist()
                        {
                            Dates = obj.DateFroms,
                            Timefrom = from,
                            Timeto = "48"
                        };
                        allDatesandtime.Add(dt);
                    }
                    else if (i == allDates.Count - 1)
                    {
                        // var timeto = obj.DateTo.ToString("HH:mm");
                        var timeto = obj.Timeto.ToString();
                        timeto = timeto.Replace(":", ".");
                        int to = Convert.ToInt32(Convert.ToDecimal(timeto) * 2);
                        var dt = new CheckDatelist()
                        {
                            Dates = obj.DateTo,
                            Timefrom = 1,
                            Timeto = to
                        };
                        allDatesandtime.Add(dt);
                    }
                    else
                    {
                        var dt = new CheckDatelist()
                        {
                            Dates = allDates[i].Date,
                            Timefrom = 1,
                            Timeto = 48
                        };
                        allDatesandtime.Add(dt);

                    }
                }


            }
            else
            {
                //var timefrom = obj.DateFrom.ToString("HH:mm");
                var timefrom = obj.Timefrom.ToString();
                timefrom = timefrom.Replace(":", ".");
                int from = Convert.ToInt32(Convert.ToDecimal(timefrom) * 2);

                var timeto = obj.Timeto.ToString();
                timeto = timeto.Replace(":", ".");
                int to = Convert.ToInt32(Convert.ToDecimal(timeto) * 2);


                var dt = new CheckDatelist()
                {
                    Dates = obj.DateFroms.Date,
                    Timefrom = from,
                    Timeto = to
                };
                allDatesandtime.Add(dt);

            }

            return allDatesandtime;
        }
        private List<CheckDatelist> GetDateTimeMethod1(LogbookClass obj)
        {
            List<DateTime> allDates = new List<DateTime>();
            List<CheckDatelist> allDatesandtime = new List<CheckDatelist>();


            for (DateTime date = obj.DateFrom; date <= obj.DateTo; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            if (allDates.Count > 1)
            {
                for (int i = 0; i < allDates.Count; i++)
                {
                    if (i == 0)
                    {

                        //var timefrom = obj.DateFroms.ToString("HH:mm");
                        var timefrom = obj.TimeFrom.ToString();
                        timefrom = timefrom.Replace(":", ".");
                        int from = Convert.ToInt32(Convert.ToDecimal(timefrom) * 2);

                        var dt = new CheckDatelist()
                        {
                            Dates = obj.DateFrom,
                            Timefrom = from,
                            Timeto = "48"
                        };
                        allDatesandtime.Add(dt);
                    }
                    else if (i == allDates.Count - 1)
                    {
                        // var timeto = obj.DateTo.ToString("HH:mm");
                        var timeto = obj.TimeTo.ToString();
                        timeto = timeto.Replace(":", ".");
                        int to = Convert.ToInt32(Convert.ToDecimal(timeto) * 2);
                        var dt = new CheckDatelist()
                        {
                            Dates = obj.DateTo,
                            Timefrom = 1,
                            Timeto = to
                        };
                        allDatesandtime.Add(dt);
                    }
                    else
                    {
                        var dt = new CheckDatelist()
                        {
                            Dates = allDates[i].Date,
                            Timefrom = 1,
                            Timeto = 48
                        };
                        allDatesandtime.Add(dt);

                    }
                }


            }
            else
            {
                //var timefrom = obj.DateFrom.ToString("HH:mm");
                var timefrom = obj.TimeFrom.ToString();
                timefrom = timefrom.Replace(":", ".");
                int from = Convert.ToInt32(Convert.ToDecimal(timefrom) * 2);

                var timeto = obj.TimeTo.ToString();
                timeto = timeto.Replace(":", ".");
                int to = Convert.ToInt32(Convert.ToDecimal(timeto) * 2);


                var dt = new CheckDatelist()
                {
                    Dates = obj.DateFrom.Date,
                    Timefrom = from,
                    Timeto = to
                };
                allDatesandtime.Add(dt);

            }

            return allDatesandtime;
        }

        private void SaveMethod(LogbookClass obj)
        {
            try
            {
                erinfo = 1;

                LogStatus obj1 = new LogStatus();

                var bbb = addPeopleList;

                var bb = AddPeopleList.Select(p => p.Position).ToList();
                obj.CrewGroup = bb != null ? string.Join(",", bb) : string.Empty;
                obj.CrewGroup = obj.CrewGroup.TrimEnd(',');


                var bb1 = AddPeopleList.Select(p => p.Position1).ToList();
                obj.CrewGroup1 = bb1 != null ? string.Join(",", bb1) : string.Empty;
                obj.CrewGroup1 = obj.CrewGroup1.TrimEnd(',');

                var bb2 = AddPeopleList.Select(p => p.UserName).ToList();
                obj.UserName = bb2 != null ? string.Join(",", bb2) : string.Empty;
                obj.UserName = obj.UserName.TrimEnd(',');

                var bb3 = AddPeopleList.Select(p => new { p.DateFroms, p.DateTo, p.DateFroms1, p.DateTo1, p.Timefrom, p.Timeto, p.DateIDFrom, p.DateIDTo, p.IDLFrom, p.IDLTo }).ToList();
                AddPeopleList.ToList().ForEach(x => { x.IDLFrom = x.IDLFrom == null ? string.Empty : x.IDLFrom; x.IDLTo = x.IDLTo == null ? string.Empty : x.IDLTo; });

                obj.DateFrom1 = bb3 != null ? string.Join(",", bb3.Select(s => s.DateFroms)) : string.Empty;
                obj.DateFrom1 = obj.DateFrom1.TrimEnd(',');
                obj.DateTo1 = bb3 != null ? string.Join(",", bb3.Select(s => s.DateTo)) : string.Empty;
                obj.DateTo1 = obj.DateTo1.TrimEnd(',');

                obj.TimeFrom = bb3 != null ? string.Join(",", bb3.Select(s => s.Timefrom)) : string.Empty;
                obj.TimeFrom = obj.TimeFrom.TrimEnd(',');
                obj.TimeTo = bb3 != null ? string.Join(",", bb3.Select(s => s.Timeto)) : string.Empty;
                obj.TimeTo = obj.TimeTo.TrimEnd(',');

                obj.IDLFrom = bb3 != null ? string.Join(",", bb3.Select(s => s.DateIDFrom)) : string.Empty;
                obj.IDLFrom = obj.IDLFrom.TrimEnd(',');

                obj.IDLTo = bb3 != null ? string.Join(",", bb3.Select(s => s.DateIDTo)) : string.Empty;
                obj.IDLTo = obj.IDLTo.TrimEnd(',');


                refreshmessage(logbookStatus);
                refreshmessage1(logbookStatus);

                if (CheckErrorMessage.CheckErrorMessages)
                {

                    List<CheckDatelist> allDatesandtime = GetDateTimeMethod1(obj);
                    string checkStatus = string.Empty;

                    var crewgroup= obj.CrewGroup.Split(',');
                    var crewgroup1 = obj.CrewGroup1.Split(',');
                    var username = obj.UserName.Split(',');
                    var datefrom1 = obj.DateFrom1.Split(',');
                    var dateto1 = obj.DateTo1.Split(',');
                    var timefrom = obj.TimeFrom.Split(',');

                    var timeto = obj.TimeTo.Split(',');
                    var idlfrom = obj.IDLFrom.Split(',');
                    var idlto = obj.TimeFrom.Split(',');
                    

                    for (int i = 0; i < username.Length - 1; i++)
                    {

                        LogbookClass obb = new LogbookClass();

                        obb.UserName = username[i];
                        obb.CrewGroup = crewgroup[i];
                        obb.CrewGroup1 = crewgroup1[i];



                        var idlf = idlfrom[i];
                        var idlt = idlto[i];


                    }

                    foreach (var item in username)
                    {
                        foreach (var dt in allDatesandtime)
                        {
                            var data = WorkHoursList.Where(x => x.UserName == item && x.dates == dt.Dates).ToList();

                            if (data == null)
                            {
                                checkStatus += item + ",";
                            }
                            else
                            {
                                foreach (var user in data)
                                {
                                    var s2 = user.hrs.TrimEnd(',').Split(',');

                                    var skip = Convert.ToInt32(dt.Timefrom);
                                    var take = Convert.ToInt32(dt.Timeto) - Convert.ToInt32(dt.Timefrom);

                                    s2 = s2.Skip(skip).Select(i => i.ToString()).ToArray();
                                    s2 = s2.Take(take).Select(i => i.ToString()).ToArray();

                                    bool exists2 = s2.Contains("0");

                                    if (exists2)
                                    {
                                        checkStatus += item + ",";
                                    }
                                }
                            }


                        }

                    }

                    var check = checkStatus.TrimEnd(',').Split(',');
                    if (!string.IsNullOrEmpty(string.Join(",", check)))
                    {
                        var users = sc.CrewDetails.Where(x => check.Contains(x.UserName)).Distinct().ToList();

                        string status = string.Empty;
                        foreach (var item in check)
                        {
                            var tm = users.Where(x => x.UserName == item).FirstOrDefault();
                            status += tm.position + "(" + tm.UserName + ")" + ",";
                        }

                        string[] bbs = status.TrimEnd(',').Split(',');
                        bbs = bbs.Distinct().ToArray();

                        obj.Status = string.Join(",", bbs);


                        sc.Logbooks.Add(obj);
                        sc.SaveChanges();

                        MessageBox.Show("Data Saved Successfully");

                        erinfo = 0;
                        //AllSelected = false;
                        //RaisePropertyChanged("AllSelected");

                        logbookStatus = new LogbookClass();
                        RaisePropertyChanged("LogbookStatus");
                        CheckErrorMessage.CheckErrorMessages = false;
                    }
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void AddMethod(LogStatus obj)
        {
            if (obj != null)
            {

                peopleList.ToList().ForEach(x => { x.DateFroms = Convert.ToDateTime(DateFroms); x.DateTo = Convert.ToDateTime(DateTos); x.Timefrom = x.Timefrom == null ? TimeFrom : x.Timefrom; x.Timeto = x.Timeto == null ? TimeTo : x.Timeto; });

                var addlist = peopleList.Where(s => s.UserName == obj.UserName).ToList();
                addlist.ForEach(x => { x.DateFroms1 = !string.IsNullOrEmpty(IDLFrom) ? Convert.ToDateTime(DateFroms).ToString("dd-MMM-yyyy") + " (" + IDLFrom + ")" : Convert.ToDateTime(DateFroms).ToString("dd-MMM-yyyy"); x.DateTo1 = !string.IsNullOrEmpty(IDLTo) ? Convert.ToDateTime(DateTos).ToString("dd-MMM-yyyy") + " (" + IDLTo + ")" : Convert.ToDateTime(DateTos).ToString("dd-MMM-yyyy"); x.DateIDFrom = DateIDFrom; x.DateIDTo = DateIDTo; x.IDLFrom = IDLFrom; x.IDLTo = IDLTo; });
                bool containsItem = addPeopleList.Any(item => item.UserName == obj.UserName);

                // addPeopleList.Clear();
                if (!containsItem)
                    addlist.ToList().ForEach(addPeopleList.Add);
                else
                    MessageBox.Show("This Crew Group is already added ", "", MessageBoxButton.OK, MessageBoxImage.Warning);

                OnPropertyChanged(new PropertyChangedEventArgs("AddPeopleList"));
            }
            else
            {
                MessageBox.Show("Please Select the CrewGroup", "", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }


        private void CancelMethod()
        {
            try
            {
                new LogbookVerificationViewModel();
                new CrewDetailViewModel();
                erinfo = 0;
                //AllSelected = false;
                //RaisePropertyChanged("AllSelected");

                logbookStatus = new LogbookClass();
                RaisePropertyChanged("LogbookStatus");
                CheckErrorMessage.CheckErrorMessages = false;

                ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private ICommand cacelCommand;
        public ICommand CancelCommand
        {
            get { return cacelCommand; }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand; }

        }

        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand;
            }

        }



        private ICommand statusCommand;
        public ICommand StatusCommand
        {
            get { return statusCommand; }
        }

        private LogbookClass logbookStatus = new LogbookClass();
        public LogbookClass LogbookStatus
        {
            get
            {
                if (erinfo == 1)
                {
                    refreshmessage(logbookStatus);
                    refreshmessage1(logbookStatus);
                    RaisePropertyChanged("AddGroupMessage");
                }
                return logbookStatus;
            }
            set
            {
                logbookStatus = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LogbookStatus"));
            }
        }


        //DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private Nullable<DateTime> _DateFroms;
        public Nullable<DateTime> DateFroms
        {
            get
            {
                if (_DateFroms == null)
                {
                    _DateFroms = DateTime.Now;

                }

                logbookStatus.DateFrom = Convert.ToDateTime(Convert.ToDateTime(_DateFroms).ToShortDateString() + " " + timefrom);
                RaisePropertyChanged("LogbookStatus");
                return _DateFroms;
            }
            set
            {
                _DateFroms = value;
                GetRankList(_DateFroms);
                RaisePropertyChanged("DateFroms");
            }
        }

        private static Nullable<DateTime> _DateTos;
        public Nullable<DateTime> DateTos
        {
            get
            {
                if (_DateTos == null)
                {
                    _DateTos = DateTime.Now;

                }

                logbookStatus.DateTo = Convert.ToDateTime(Convert.ToDateTime(_DateTos).ToShortDateString() + " " + timeto);
                RaisePropertyChanged("LogbookStatus");
                return _DateTos;
            }
            set
            {
                _DateTos = value;
                GetRankList(_DateTos);
                RaisePropertyChanged("DateTos");
            }
        }




        private static string timefrom = "12:00";
        public string TimeFrom
        {
            get
            {
                logbookStatus.DateFrom = Convert.ToDateTime(Convert.ToDateTime(_DateFroms).ToShortDateString() + " " + timefrom);
                return timefrom;
            }
            set
            {
                timefrom = value;
                RaisePropertyChanged("TimeFrom");
            }
        }
        private static string timeto = "12:30";
        public string TimeTo
        {
            get
            {
                logbookStatus.DateTo = Convert.ToDateTime(Convert.ToDateTime(_DateTos).ToShortDateString() + " " + timeto);
                return timeto;
            }
            set
            {
                timeto = value;
                RaisePropertyChanged("TimeTo");
            }
        }

        private string dateIDFrom;
        public string DateIDFrom
        {
            get
            {
                return dateIDFrom;
            }
            set
            {
                dateIDFrom = value;
                RaisePropertyChanged("DateIDFrom");
            }
        }

        private string dateIDTo;
        public string DateIDTo
        {
            get
            {
                return dateIDTo;
            }
            set
            {
                dateIDTo = value;
                RaisePropertyChanged("DateIDTo");
            }
        }

        private string iDLFrom;
        public string IDLFrom
        {
            get
            {
                return iDLFrom;
            }
            set
            {
                iDLFrom = value;
                RaisePropertyChanged("IDLFrom");
            }
        }

        private string iDLTo;
        public string IDLTo
        {
            get
            {
                return iDLTo;
            }
            set
            {
                iDLTo = value;
                RaisePropertyChanged("IDLTo");
            }
        }



        private static ObservableCollection<LogStatus> addPeopleList = new ObservableCollection<LogStatus>();
        public ObservableCollection<LogStatus> AddPeopleList
        {
            get
            {
                return addPeopleList;
            }
            set
            {
                addPeopleList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddPeopleList"));


            }
        }





        //public static ObservableCollection<GridClass> loadCalender = new ObservableCollection<GridClass>();
        //public ObservableCollection<GridClass> LoadCalender
        //{
        //    get
        //    {
        //        return loadCalender;
        //    }
        //    set
        //    {
        //        loadCalender = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("LoadCalender"));

        //    }
        //}

        //private static ObservableCollection<string> yearname = new ObservableCollection<string>();
        //public ObservableCollection<string> YearName
        //{
        //    get
        //    {
        //        return yearname;
        //    }
        //    set
        //    {
        //        yearname = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("YearName"));
        //    }
        //}

        //private static ObservableCollection<string> monthname = new ObservableCollection<string>();
        //public ObservableCollection<string> MonthName
        //{
        //    get
        //    {
        //        return monthname;
        //    }
        //    set
        //    {
        //        monthname = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("MonthName"));
        //    }
        //}


        //private static string syearname;
        //public string SYearName
        //{
        //    get
        //    {
        //        if (syearname == null)
        //            syearname = DateTime.Now.Year.ToString();
        //        BindCalender(syearname, smonthname);
        //        return syearname;
        //    }

        //    set
        //    {
        //        syearname = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("SYearName"));
        //    }
        //}

        //private static string smonthname;
        //public string SMonthName
        //{
        //    get
        //    {
        //        if (smonthname == null)
        //            smonthname = DateTime.Now.ToString("MMMM");
        //        BindCalender(syearname, smonthname);
        //        return smonthname;
        //    }

        //    set
        //    {
        //        smonthname = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("SMonthName"));
        //    }
        //}


        //public static ObservableCollection<GridClass> loadCalenderTo = new ObservableCollection<GridClass>();
        //public ObservableCollection<GridClass> LoadCalenderTo
        //{
        //    get
        //    {
        //        return loadCalenderTo;
        //    }
        //    set
        //    {
        //        loadCalenderTo = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("LoadCalenderTo"));

        //    }
        //}

        //private static ObservableCollection<string> yearnameTo = new ObservableCollection<string>();
        //public ObservableCollection<string> YearNameTo
        //{
        //    get
        //    {
        //        return yearnameTo;
        //    }
        //    set
        //    {
        //        yearnameTo = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("YearNameTo"));
        //    }
        //}

        //private static ObservableCollection<string> monthnameTo = new ObservableCollection<string>();
        //public ObservableCollection<string> MonthNameTo
        //{
        //    get
        //    {
        //        return monthnameTo;
        //    }
        //    set
        //    {
        //        monthnameTo = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("MonthNameTo"));
        //    }
        //}


        //private static string syearnameTo;
        //public string SYearNameTo
        //{
        //    get
        //    {
        //        if (syearnameTo == null)
        //            syearnameTo = DateTime.Now.Year.ToString();
        //        BindCalenderTo(syearnameTo, smonthnameTo);
        //        return syearnameTo;
        //    }

        //    set
        //    {
        //        syearnameTo = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("SYearNameTo"));
        //    }
        //}

        //private static string smonthnameTo;
        //public string SMonthNameTo
        //{
        //    get
        //    {
        //        if (smonthnameTo == null)
        //            smonthnameTo = DateTime.Now.ToString("MMMM");
        //        BindCalenderTo(syearnameTo, smonthnameTo);
        //        return smonthnameTo;
        //    }

        //    set
        //    {
        //        smonthnameTo = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("SMonthNameTo"));
        //    }
        //}



        //private void BindCalender(string syearname, string smonthname)
        //{
        //    string[] arr = sc.CommonCalender(syearname, smonthname);

        //    int fularr = arr.Count();
        //    loadCalender.Clear();

        //    for (int k = 0; k < fularr; k++)
        //    {


        //        loadCalender.Add(new GridClass() { Day1 = fularr - k >= 0 ? arr[k] : string.Empty, Day2 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day3 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day4 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day5 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day6 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day7 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty });


        //    }

        //}

        //private void BindCalenderTo(string syearname, string smonthname)
        //{
        //    string[] arr = sc.CommonCalender(syearname, smonthname);

        //    int fularr = arr.Count();
        //    loadCalenderTo.Clear();

        //    for (int k = 0; k < fularr; k++)
        //    {


        //        loadCalenderTo.Add(new GridClass() { Day1 = fularr - k >= 0 ? arr[k] : string.Empty, Day2 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day3 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day4 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day5 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day6 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day7 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty });


        //    }

        //}



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }

    public class LogStatus
    {
        public string Position { get; set; }
        public string Position1 { get; set; }
        public string UserName { get; set; }

        public bool IsChecked { get; set; }

        public DateTime DateFroms { get; set; }
        public DateTime DateTo { get; set; }
        public object Timefrom { get; set; }
        public object Timeto { get; set; }


        public object DateFroms1 { get; set; }
        public object DateTo1 { get; set; }
        public string DateIDFrom { get; set; }
        public string DateIDTo { get; set; }
        public string IDLFrom { get; set; }
        public string IDLTo { get; set; }

    }

    class CheckDatelist
    {
        public DateTime Dates { get; set; }
        public object Timefrom { get; set; }
        public object Timeto { get; set; }

    }
}
