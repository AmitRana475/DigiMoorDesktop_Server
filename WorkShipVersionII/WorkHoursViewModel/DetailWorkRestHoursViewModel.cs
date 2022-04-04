using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class DetailWorkRestHoursViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;
        public DetailWorkRestHoursViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;

            }

            if (UserTypeClass.UserTypes == "HOD")
            {
                string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                var data = sc.GetCrewDetail(searchCrew).ToList();
                data = data.Where(x => depart.Contains(x.department)).ToList();

                var data1 = sc.GetCrewDetail1(searchCrew1).ToList();
                data1 = data1.Where(x => depart.Contains(x.department)).ToList();

                loadCrewDetails.Clear();
                sc.ObservableCollectionList(loadCrewDetails, data);
                loadCrewDetails1.Clear();
                sc.ObservableCollectionList(loadCrewDetails1, data1);
            }
            else if (UserTypeClass.UserTypes == "Crew")
            {
                loadCrewDetails.Clear();
                sc.ObservableCollectionList(loadCrewDetails, sc.GetCrewDetail(searchCrew));
                loadCrewDetails1.Clear();
                sc.ObservableCollectionList(loadCrewDetails1, sc.GetCrewDetail1(searchCrew1));
            }
            else
            {
                loadCrewDetails.Clear();
                sc.ObservableCollectionList(loadCrewDetails, sc.GetCrewDetail(searchCrew));
                loadCrewDetails1.Clear();
                sc.ObservableCollectionList(loadCrewDetails1, sc.GetCrewDetail1(searchCrew1));
            }

          
        }


        private static string searchCrew;
        public string SearchCrews
        {
            get
            {
                if (searchCrew != null)
                {
                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                        var data = sc.GetCrewDetail(searchCrew).ToList();
                        data = data.Where(x => depart.Contains(x.department)).ToList();

                        loadCrewDetails.Clear();
                        sc.ObservableCollectionList(loadCrewDetails, data);
                        RaisePropertyChanged("LoadCrewDetails");

                    }
                    else
                    {


                        loadCrewDetails.Clear();
                        sc.ObservableCollectionList(loadCrewDetails, sc.GetCrewDetail(searchCrew));
                        RaisePropertyChanged("LoadCrewDetails");
                    }
                }
                return searchCrew;
            }

            set
            {
                searchCrew = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrews"));
            }
        }

        private static string searchCrew1;
        public string SearchCrews1
        {
            get
            {
                if (searchCrew1 != null)
                {
                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                        var data = sc.GetCrewDetail1(searchCrew1).ToList();
                        data = data.Where(x => depart.Contains(x.department)).ToList();

                        loadCrewDetails1.Clear();
                        sc.ObservableCollectionList(loadCrewDetails1, data);
                        RaisePropertyChanged("LoadCrewDetails1");

                    }
                    else
                    {

                        loadCrewDetails1.Clear();
                        sc.ObservableCollectionList(loadCrewDetails1, sc.GetCrewDetail1(searchCrew1));
                        RaisePropertyChanged("LoadCrewDetails1");
                    }
                }
                return searchCrew1;
            }

            set
            {
                searchCrew1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrews1"));
            }
        }

        private static ObservableCollection<CrewDetailClass> loadCrewDetails = new ObservableCollection<CrewDetailClass>();
        public ObservableCollection<CrewDetailClass> LoadCrewDetails
        {
            get
            {
                return loadCrewDetails;
            }
            set
            {
                loadCrewDetails = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrewDetails"));
            }
        }
        private static ObservableCollection<CrewDetailClass> loadCrewDetails1 = new ObservableCollection<CrewDetailClass>();
        public ObservableCollection<CrewDetailClass> LoadCrewDetails1
        {
            get
            {
                return loadCrewDetails1;
            }
            set
            {
                loadCrewDetails1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrewDetails1"));
            }
        }




        // ON SIGNBOARD........


        //private List<CrewDetailClass> GetCrewDetail(string searchCrew)
        //{
        //    try
        //    {
        //        DateTime dd = DateTime.Now.Date;
        //        var data = sc.CrewDetails.Where(p => p.ServiceTo >= dd).ToList();

        //        if (!string.IsNullOrEmpty(searchCrew))
        //        {
        //            data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower())).ToList();

        //        }
        //        var workhrs1 = sc.WorkHourss.ToList();
        //        var workhrs = workhrs1.Select(s => new WorkHoursClass { wid = s.wid, UserName = s.UserName, dates = s.dates, Last_update = s.Last_update }).Where(s => s.Last_update == workhrs1.Max(x => x.Last_update)).GroupBy(n => n.UserName).Select(g => g.FirstOrDefault()).ToList();

        //        var LeftJoinData = (from emp in data
        //                            join dept in workhrs
        //                            on emp.UserName equals dept.UserName into JoinedEmpDept
        //                            from dept in JoinedEmpDept.DefaultIfEmpty()
        //                            select new CrewDetailClass()
        //                            {
        //                                Id = emp.Id,
        //                                name = emp.name,
        //                                UserName = emp.UserName,
        //                                position = emp.position,
        //                                department = emp.department,
        //                                ServiceFrom = emp.ServiceFrom,
        //                                ServiceTo = emp.ServiceTo,
        //                                CDC = emp.CDC,
        //                                empno = emp.empno,
        //                                DateAvailble = dept != null ? Convert.ToDateTime(dept.Last_update).ToString("dd-MMM-yyyy") : "No Entry"
        //                            }).ToList();


        //        return LeftJoinData;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        return null;
        //    }
        //}


        // OFF SIGNBOARD........

        //private ObservableCollection<CrewDetailClass> GetCrewDetail1_old(string searchCrew)
        //{
        //    DateTime dd = DateTime.Now.Date;
        //    var data = sc.CrewDetails.Where(p => p.ServiceTo < dd).Select(x => new { x.Id, x.name, x.UserName, x.position, x.department, x.ServiceFrom, x.ServiceTo, x.CDC, x.empno });

        //    if (!string.IsNullOrEmpty(searchCrew))
        //    {
        //        data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower()));
        //    }

        //    var ranklist = new ObservableCollection<CrewDetailClass>();


        //    foreach (var item in data.ToList())
        //    {
        //        ranklist.Add(new CrewDetailClass() { Id = item.Id, name = item.name, UserName = item.UserName, position = item.position, department = item.department, ServiceFrom = item.ServiceFrom, ServiceTo = item.ServiceTo, CDC = item.CDC, empno = item.empno });
        //    }

        //    return ranklist;
        //}


        //private List<CrewDetailClass> GetCrewDetail1(string searchCrew)
        //{
        //    try
        //    {
        //        DateTime dd = DateTime.Now.Date;
        //        var data = sc.CrewDetails.Where(p => p.ServiceTo < dd).ToList();

        //        if (!string.IsNullOrEmpty(searchCrew))
        //        {
        //            data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower())).ToList();
        //        }

        //        var workhrs1 = sc.WorkHourss.ToList();
        //        var workhrs = workhrs1.Select(s => new WorkHoursClass { wid = s.wid, UserName = s.UserName, dates = s.dates, Last_update = s.Last_update }).Where(s => s.Last_update == workhrs1.Max(x => x.Last_update)).GroupBy(n => n.UserName).Select(g => g.FirstOrDefault()).ToList();

        //        var LeftJoinData = (from emp in data
        //                            join dept in workhrs
        //                            on emp.UserName equals dept.UserName into JoinedEmpDept
        //                            from dept in JoinedEmpDept.DefaultIfEmpty()
        //                            select new CrewDetailClass()
        //                            {
        //                                Id = emp.Id,
        //                                name = emp.name,
        //                                UserName = emp.UserName,
        //                                position = emp.position,
        //                                department = emp.department,
        //                                ServiceFrom = emp.ServiceFrom,
        //                                ServiceTo = emp.ServiceTo,
        //                                CDC = emp.CDC,
        //                                empno = emp.empno,
        //                                DateAvailble = dept != null ? Convert.ToDateTime(dept.Last_update).ToString("dd-MMM-yyyy") : "No Entry"
        //                            }).ToList();


        //        return LeftJoinData;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
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
