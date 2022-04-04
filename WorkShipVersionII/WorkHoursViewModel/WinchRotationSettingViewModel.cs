using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class WinchRotationSettingViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              public ICommand HelpCommand { get; private set; }
              public WinchRotationSettingViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     //LoadUserAccess.Clear();

                     //var lostdata = new ObservableCollection<MOperationBirthDetail>(sc.MOperationBirthDetailTbl.ToList());
                     //MooringOPRListingViewModel cc = new MooringOPRListingViewModel(lostdata);

                     GetWinchRotationSettingList();
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
              }

              public static ObservableCollection<WinchRotationSettingClass> loadUserAccess = new ObservableCollection<WinchRotationSettingClass>();
              public ObservableCollection<WinchRotationSettingClass> LoadUserAccess
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


              public void GetWinchRotationSettingList()
              {
                     try
                     {
                            LoadUserAccess.Clear();
                            //SqlCommand cmd = new SqlCommand("GetDamageRope", sc.con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@RopeTail", 0);
                            string cmd = "select a.*,b.RopeType,m.Name from tblWinchRotationSetting a join MooringRopeType b on a.MooringRopeType=b.Id join tblCommon m on m.Id=a.ManufacturerType";
                            SqlDataAdapter adp = new SqlDataAdapter(cmd,sc.con);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new WinchRotationSettingClass()
                                   {
                                          Id = (int)row["Id"],
                                          MooringRopeType = (int)row["MooringRopeType"],
                                          ManufacturerType = (int)row["ManufacturerType"],
                                          MaximumRunningHours = (int)row["MaximumRunningHours"],
                                          MaximumMonthsAllowed = (int)row["MaximumMonthsAllowed"],
                                          LeadFrom = (string)row["LeadFrom"],
                                          LeadTo = (string)row["LeadTo"],
                                          RopeType = (string)row["RopeType"],
                                          Manufacturer = (string)row["Name"],

                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

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
