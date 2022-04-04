using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShipVersionII.ViewModelReports
{
    public class DepartmentPlanningViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public DepartmentPlanningViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            Items = GetDepartment();
            OnPropertyChanged(new PropertyChangedEventArgs("Items"));

            SelectedItems = GetDepartment();
            SelectedItems.Add("All", "0");
            OnPropertyChanged(new PropertyChangedEventArgs("SelectedItems"));


        }

        private Dictionary<string, object> GetDepartment()
        {
            var AddDepartments = new Dictionary<string, object>();
            var data = sc.Departments.Select(x => x.DeptName).ToList();
            int counter = 0;

            foreach (var item in data)
            {
                counter++;
                AddDepartments.Add(item, counter);

            }

            return AddDepartments;
        }

        private Dictionary<string, object> _items;
        private Dictionary<string, object> _selectedItems;

        public Dictionary<string, object> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                RaisePropertyChanged("Items");
            }
        }

        public Dictionary<string, object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }

        private static Nullable<DateTime> _ServiceFrom = null;
        public Nullable<DateTime> ServiceFrom
        {
            get
            {
                if (_ServiceFrom == null)
                {
                    _ServiceFrom = DateTime.Now;
                }
                return _ServiceFrom;
            }
            set
            {
                _ServiceFrom = value;
                RaisePropertyChanged("ServiceFrom");
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
