using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelReports
{
    public class CrewDetailViewModel : ViewModelBase
    {
        private static ShipmentContaxt sc;
        bool ischeck = false;
        string usename = "";
        public CrewDetailViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }


            if (UserTypeClass.UserTypes == "HOD")
            {
                string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                loadCrewDetailr.Clear();
                var list = sc.GetCrewDetail(searchCrew);
                list = list.Where(x => depart.Contains(x.department)).ToList();
                list.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });

                sc.ObservableCollectionList(loadCrewDetailr, list);


                loadCrewDetailr1.Clear();

                var list1 = sc.GetCrewDetail1(searchCrew1);
                list1 = list1.Where(x => depart.Contains(x.department)).ToList();
                list1.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });

                sc.ObservableCollectionList(loadCrewDetailr1, list1);

            }
            else
            {

                loadCrewDetailr.Clear();
                var list = sc.GetCrewDetail(searchCrew);
                list.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                sc.ObservableCollectionList(loadCrewDetailr, list);

                loadCrewDetailr1.Clear();
                var list1 = sc.GetCrewDetail1(searchCrew1);
                list1.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                sc.ObservableCollectionList(loadCrewDetailr1, list1);

            }

            onCheckedCommand = new RelayCommand<CrewDetailClass>(OnCheckedMethod);

            //loadCrewDetailr1 = GetCrewDetail1(searchCrew1);
            //loadCrewDetailr = GetCrewDetail(searchCrew);
        }

        private void OnCheckedMethod(CrewDetailClass obj)
        {
            try
            {
                ischeck = obj.IsChecked;
                usename = obj.UserName;


                //loadCrewDetailr = GetCrewDetail(searchCrew);

                if (UserTypeClass.UserTypes == "HOD")
                {
                    string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                    loadCrewDetailr.Clear();
                    var list = sc.GetCrewDetail(searchCrew);
                    list = list.Where(x => depart.Contains(x.department)).ToList();
                    list.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                    sc.ObservableCollectionList(loadCrewDetailr, list);
                    RaisePropertyChanged("LoadCrewDetailr");

                    loadCrewDetailr1.Clear();
                    var list1 = sc.GetCrewDetail1(searchCrew1);
                    list1 = list1.Where(x => depart.Contains(x.department)).ToList();
                    list1.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                    sc.ObservableCollectionList(loadCrewDetailr1, list1);
                    RaisePropertyChanged("LoadCrewDetailr1");

                }
                else
                {

                    loadCrewDetailr.Clear();
                    var list = sc.GetCrewDetail(searchCrew);
                    list.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                    sc.ObservableCollectionList(loadCrewDetailr, list);
                    RaisePropertyChanged("LoadCrewDetailr");

                    //loadCrewDetailr1 = GetCrewDetail1(searchCrew1);
                    loadCrewDetailr1.Clear();
                    var list1 = sc.GetCrewDetail1(searchCrew1);
                    list1.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                    sc.ObservableCollectionList(loadCrewDetailr1, list1);
                    RaisePropertyChanged("LoadCrewDetailr1");
                }
                if (ischeck)
                    MainViewModelReports._CommonData.ReportVisibles = true;
                else
                    MainViewModelReports._CommonData.ReportVisibles = false;
                RaisePropertyChanged("ReportVisibles");

                obj = sc.CrewDetails.Where(x => x.UserName.ToLower() == obj.UserName.ToLower() && x.position == obj.position).FirstOrDefault();

                MainViewModelReports._CommonData.GetUserDetail = obj;
                RaisePropertyChanged("GetUserDetail");


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }

        }

        private ICommand onCheckedCommand;
        public ICommand OnCheckedCommand
        {
            get { return onCheckedCommand; }
        }
        private static string searchCrew;
        public string SearchCrewr
        {
            get
            {
                if (searchCrew != null)
                {

                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                        loadCrewDetailr.Clear();
                        var list = sc.GetCrewDetail(searchCrew);
                        list = list.Where(x => depart.Contains(x.department)).ToList();
                        list.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                        sc.ObservableCollectionList(loadCrewDetailr, list);
                        RaisePropertyChanged("LoadCrewDetailr");

                    }
                    else
                    {

                        loadCrewDetailr.Clear();
                        var list = sc.GetCrewDetail(searchCrew);
                        list.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                        sc.ObservableCollectionList(loadCrewDetailr, list);
                        RaisePropertyChanged("LoadCrewDetailr");
                    }
                }
                return searchCrew;
            }

            set
            {
                searchCrew = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrewr"));
            }
        }

        private static string searchCrew1;
        public string SearchCrewr1
        {
            get
            {
                if (searchCrew1 != null)
                {

                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                        LoadCrewDetailr1.Clear();
                        var list1 = sc.GetCrewDetail1(searchCrew1);
                        list1 = list1.Where(x => depart.Contains(x.department)).ToList();
                        list1.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                        sc.ObservableCollectionList(LoadCrewDetailr1, list1);
                        RaisePropertyChanged("LoadCrewDetailr1");
                    }
                    else
                    {
                        LoadCrewDetailr1.Clear();
                        var list1 = sc.GetCrewDetail1(searchCrew1);
                        list1.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });
                        sc.ObservableCollectionList(LoadCrewDetailr1, list1);
                        RaisePropertyChanged("LoadCrewDetailr1");
                    }
                }
                return searchCrew1;
            }

            set
            {
                searchCrew1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrewr1"));
            }
        }

        public static ObservableCollection<CrewDetailClass> loadCrewDetailr = new ObservableCollection<CrewDetailClass>();
        public ObservableCollection<CrewDetailClass> LoadCrewDetailr
        {
            get
            {
                return loadCrewDetailr;
            }
            set
            {
                loadCrewDetailr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrewDetailr"));
            }
        }
        private static ObservableCollection<CrewDetailClass> loadCrewDetailr1 = new ObservableCollection<CrewDetailClass>();
        public ObservableCollection<CrewDetailClass> LoadCrewDetailr1
        {
            get
            {
                return loadCrewDetailr1;
            }
            set
            {
                loadCrewDetailr1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrewDetailr1"));
            }
        }

        //// ON SIGNBOARD........
        //private List<CrewDetailClass> GetCrewDetail(string searchCrew)
        //{
        //    //var ranklist = new ObservableCollection<CrewDetailClass>();
        //    try
        //    {
        //        DateTime dd = DateTime.Now.Date;

        //        //var data = sc.CrewDetails.Where(p => p.ServiceTo >= dd).Select(x => new { x.Id, x.name, x.UserName, x.position, x.department, x.ServiceFrom, x.ServiceTo, x.CDC, x.empno });
        //        var data = sc.CrewDetails.Where(p => p.ServiceTo >= dd).ToList();

        //        if (!string.IsNullOrEmpty(searchCrew))
        //        {
        //            data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower())).ToList();

        //        }

        //        data.ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });


        //        //foreach (var item in data.ToList())
        //        //{
        //        //    if (item.UserName.Equals(usename) && ischeck)
        //        //    {
        //        //        ranklist.Add(new CrewDetailClass() { Id = item.Id, name = item.name, UserName = item.UserName, position = item.position, department = item.department, ServiceFrom = item.ServiceFrom, ServiceTo = item.ServiceTo, CDC = item.CDC, empno = item.empno, IsChecked = true });
        //        //    }
        //        //    else
        //        //    {
        //        //        ranklist.Add(new CrewDetailClass() { Id = item.Id, name = item.name, UserName = item.UserName, position = item.position, department = item.department, ServiceFrom = item.ServiceFrom, ServiceTo = item.ServiceTo, CDC = item.CDC, empno = item.empno, IsChecked = false });
        //        //    }

        //        //}

        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        // ranklist.Clear();
        //        return null;
        //    }


        //}





        //// OFF SIGNBOARD........
        //private List<CrewDetailClass> GetCrewDetail1(string searchCrew)
        //{
        //   // var ranklist = new ObservableCollection<CrewDetailClass>();
        //    try
        //    {
        //        DateTime dd = DateTime.Now.Date;
        //        //var data = sc.CrewDetails.Where(p => p.ServiceTo < dd).Select(x => new { x.Id, x.name, x.UserName, x.position, x.department, x.ServiceFrom, x.ServiceTo, x.CDC, x.empno ,x.IsChecked});

        //        var data = sc.CrewDetails.Where(p => p.ServiceTo < dd).ToList();

        //        if (!string.IsNullOrEmpty(searchCrew))
        //        {
        //            data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower())).ToList();
        //        }


        //        data.ToList().ForEach(x => { if (x.UserName.Equals(usename) && ischeck) { x.IsChecked = true; } else { x.IsChecked = false; } });



        //        //foreach (var item in data.ToList())
        //        //{
        //        //    if (item.UserName.Equals(usename) && ischeck)
        //        //    {
        //        //        ranklist.Add(new CrewDetailClass() { Id = item.Id, name = item.name, UserName = item.UserName, position = item.position, department = item.department, ServiceFrom = item.ServiceFrom, ServiceTo = item.ServiceTo, CDC = item.CDC, empno = item.empno, IsChecked = true });
        //        //    }
        //        //    else
        //        //    {
        //        //        ranklist.Add(new CrewDetailClass() { Id = item.Id, name = item.name, UserName = item.UserName, position = item.position, department = item.department, ServiceFrom = item.ServiceFrom, ServiceTo = item.ServiceTo, CDC = item.CDC, empno = item.empno, IsChecked = false });
        //        //    }
        //        //    //ranklist.Add(new CrewDetailClass() { Id = item.Id, name = item.name, UserName = item.UserName, position = item.position, department = item.department, ServiceFrom = item.ServiceFrom, ServiceTo = item.ServiceTo, CDC = item.CDC, empno = item.empno });
        //        //}

        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        //ranklist.Clear();
        //        return null;
        //    }
        //}


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
