using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.WorkHoursViewModel;


namespace WorkShipVersionII.WorkHoursView
{
       /// <summary>
       /// Interaction logic for MooringOPRView.xaml
       /// </summary>
       public partial class MooringOPRView : UserControl
       {
              private readonly ShipmentContaxt sc;

              int depthatberth = 0;
              public MooringOPRView()
              {
                     XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
                     InitializeComponent();

            //MooringOPRViewModel._FastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //MooringOPRViewModel._CastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            MooringOPRViewModel.sfacilityname = null;
            MooringOPRViewModel.subDates.Clear();
                     MooringOPRViewModel.subDates.Add(DateTime.Now.ToShortDateString());
                     OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

                     RefreshWinchGrid();

                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     //var to = DpCastoffDate.Text + " " + cbHours1.Text + ":" + cbMints1.Text;
                     //var dateto = Convert.ToDateTime(to);



                     cbBirthType.Text = MooringOPRViewModel.mOperationBirths.BirthType;
                     MooringType.Text = MooringOPRViewModel.mOperationBirths.MooringType;
                     BearthSide.Text = MooringOPRViewModel.mOperationBirths.BerthSide;
                     CBVesselCondition.Text = MooringOPRViewModel.mOperationBirths.VesselCondition;

                     CbWindDirection.Text = MooringOPRViewModel.mOperationBirths.WindDirection;
                     CBSquall.Text = MooringOPRViewModel.mOperationBirths.AnySquall;
                     CBberthExposed.Text = MooringOPRViewModel.mOperationBirths.Berth_exposed_SeaSwell;
                     CBSurging.Text = MooringOPRViewModel.mOperationBirths.SurgingObserved;
                     CBAnyAffect.Text = MooringOPRViewModel.mOperationBirths.Any_Affect_Passing_Traffic;
                     CBShipWasContact.Text = MooringOPRViewModel.mOperationBirths.Ship_was_continuously_contact_with_fender;



                     //dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                     //    dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = Convert.ToDateTime( MooringOPRViewModel.mOperationBirths.FastDatetime).ToShortDateString();
                     //DpCastoffDate.Text = Convert.ToDateTime(MooringOPRViewModel.mOperationBirths.CastDatetime).ToShortDateString();

                     MooringOPRViewModel.mOperationBirths.Any_Rope_Damaged = "No";

                     CBShipAccess.Text = MooringOPRViewModel.mOperationBirths.ShipAccess;
                     DamagedCombo.Text = MooringOPRViewModel.mOperationBirths.Any_Rope_Damaged;
                     //DamagedCombo.Text = "No";
                     if (DamagedCombo.Text == "No")
                     {
                            //BtnAddDamaged.Visibility = Visibility.Collapsed;
                            lblSave.Text = " Save ";
                            btnSaveAll.Width = 65;
                     }
                     else
                     {
                            //BtnAddDamaged.Visibility = Visibility.Visible;
                            lblSave.Text = " Add Damaged Line ";
                            btnSaveAll.Width = 165;
                     }

                     if (MooringOPRViewModel.mOperationBirths.OPId != 0)
                     {
                            SqlDataAdapter adp = new SqlDataAdapter("select distinct GridID from MOUsedWinchTbl where OperationID=" + MooringOPRViewModel.mOperationBirths.OPId + "", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                   var gridid = dt.Rows[i][0].ToString();

                                   if (gridid == "Grid0" && dt.Rows.Count > 1)
                                   {
                                          WinchMainGrid1.IsEnabled = false;
                                          Add0.IsEnabled = false;
                                   }
                                   if (gridid == "Grid1" && dt.Rows.Count > 2)
                                   {
                                          Grid1.IsEnabled = false;
                                          Add1.IsEnabled = false;
                                          Remove1.IsEnabled = false;

                                   }
                                   if (gridid == "Grid2" && dt.Rows.Count > 3)
                                   {
                                          Grid2.IsEnabled = false;
                                          Add2.IsEnabled = false;
                                          Remove2.IsEnabled = false;
                                   }
                                   if (gridid == "Grid3" && dt.Rows.Count > 4)
                                   {
                                          Grid3.IsEnabled = false;
                                          Add3.IsEnabled = false;
                                          Remove3.IsEnabled = false;
                                   }

                            }

                     }

                     // dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                     //       dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = dateto.ToShortDateString();

                     //  MooringOPRViewModel.dpSubFastGrid1 = MooringOPRViewModel.dpSubCastGrid1 = dateto.ToShortDateString();
              }

              private void Add0_Click(object sender, RoutedEventArgs e)
              {
                     WinchMainGrid1.IsEnabled = false;
                     Add0.Visibility = Visibility.Collapsed;

                     Grid1.Visibility = Visibility.Visible;
                     Remove1.Visibility = Visibility.Visible;
                     Add1.Visibility = Visibility.Visible;

                     dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                     dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = DpFastDate.Text;


                     cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                     cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                     cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                     cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
              }

              private void Remove1_Click(object sender, RoutedEventArgs e)
              {
                     WinchMainGrid1.IsEnabled = true;
                     Add0.Visibility = Visibility.Visible;

                     Grid1.Visibility = Visibility.Collapsed;
                     Remove1.Visibility = Visibility.Collapsed;
                     Add1.Visibility = Visibility.Collapsed;

                     WinchMainGrid1.IsEnabled = true;
                     Add0.IsEnabled = true;
              }

              private void Add1_Click(object sender, RoutedEventArgs e)
              {
                     WinchGrid1.IsEnabled = false;
                     Add1.Visibility = Visibility.Collapsed;
                     Remove1.Visibility = Visibility.Collapsed;

                     Grid2.Visibility = Visibility.Visible;
                     Remove2.Visibility = Visibility.Visible;
                     Add2.Visibility = Visibility.Visible;

                     Grid1.IsEnabled = true;
                     Add1.IsEnabled = true;
                     Remove1.IsEnabled = true;
              }

              private void Remove2_Click(object sender, RoutedEventArgs e)
              {
                     WinchGrid1.IsEnabled = true;
                     Add1.Visibility = Visibility.Visible;
                     Remove1.Visibility = Visibility.Visible;

                     Grid2.Visibility = Visibility.Collapsed;
                     Remove2.Visibility = Visibility.Collapsed;
                     Add2.Visibility = Visibility.Collapsed;

                     Grid2.IsEnabled = true;
                     Add2.IsEnabled = true;
                     Remove2.IsEnabled = true;
              }

              private void Add2_Click(object sender, RoutedEventArgs e)
              {
                     WinchGrid2.IsEnabled = false;
                     Add2.Visibility = Visibility.Collapsed;
                     Remove2.Visibility = Visibility.Collapsed;

                     Grid3.Visibility = Visibility.Visible;
                     Remove3.Visibility = Visibility.Visible;
                     Add3.Visibility = Visibility.Visible;

                     Grid3.IsEnabled = true;
                     Add3.IsEnabled = true;
                     Remove3.IsEnabled = true;
              }

              private void Remove3_Click(object sender, RoutedEventArgs e)
              {
                     WinchGrid2.IsEnabled = true;
                     Add2.Visibility = Visibility.Visible;
                     Remove2.Visibility = Visibility.Visible;

                     Grid3.Visibility = Visibility.Collapsed;
                     Remove3.Visibility = Visibility.Collapsed;
                     Add3.Visibility = Visibility.Collapsed;

                     //Grid1.IsEnabled = true;
                     //Add1.IsEnabled = true;
                     //Remove1.IsEnabled = true;
              }

              private void Add3_Click(object sender, RoutedEventArgs e)
              {
                     WinchGrid3.IsEnabled = false;
                     Add3.Visibility = Visibility.Collapsed;
                     Remove3.Visibility = Visibility.Collapsed;

                     Grid4.Visibility = Visibility.Visible;
                     Remove4.Visibility = Visibility.Visible;



              }

              private void Remove4_Click(object sender, RoutedEventArgs e)
              {
                     WinchGrid3.IsEnabled = true;
                     Add3.Visibility = Visibility.Visible;
                     Remove3.Visibility = Visibility.Visible;

                     Grid4.Visibility = Visibility.Collapsed;
                     Remove4.Visibility = Visibility.Collapsed;
              }

              public void RefreshWinchGrid()
              {
                     //WinchMainGrid1.IsEnabled = true;
                     //WinchGrid1.IsEnabled = true;
                     //WinchGrid2.IsEnabled = true;
                     //WinchGrid3.IsEnabled = true;
                     //WinchGrid4.IsEnabled = true;

                     Add0.Visibility = Visibility.Visible;

                     Grid1.Visibility = Visibility.Hidden;
                     Remove1.Visibility = Visibility.Hidden;
                     Add1.Visibility = Visibility.Hidden;

                     Grid2.Visibility = Visibility.Hidden;
                     Remove2.Visibility = Visibility.Hidden;
                     Add2.Visibility = Visibility.Hidden;

                     Grid3.Visibility = Visibility.Hidden;
                     Remove3.Visibility = Visibility.Hidden;
                     Add3.Visibility = Visibility.Hidden;


                     Grid4.Visibility = Visibility.Hidden;
                     Remove4.Visibility = Visibility.Hidden;

              }

              private void DpFastDate_CalendarClosed(object sender, RoutedEventArgs e)
              {
                     try
                     {
                            var Frm = DpFastDate.Text + " " + cbHours.Text + ":" + cbMints.Text;
                            var datefrom = Convert.ToDateTime(Frm);

                            var to = DpCastoffDate.Text + " " + cbHours1.Text + ":" + cbMints1.Text;
                            var dateto = Convert.ToDateTime(to);

                            if (datefrom > dateto)
                            {
                                   MessageBox.Show("Invalid Date Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                                   DpFastDate.Text = DateTime.Now.ToShortDateString();
                                   DpCastoffDate.Text = DateTime.Now.ToShortDateString();

                                   cbHours.SelectedIndex = 0;
                                   cbMints.SelectedIndex = 0;

                                   cbHours1.SelectedIndex = 0;
                                   cbMints1.SelectedIndex = 0;

                                   cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                                   cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                                   cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                                   cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                            }
                            else
                            {

                                   DateTime cd = dateto.Date;// Convert.ToDateTime(to);
                                   DateTime fd = datefrom.Date;//Convert.ToDateTime(Frm);
                                   int Day_Diff = (int)(cd - fd).TotalDays;

                                   MooringOPRViewModel.subDates.Clear();
                                   OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                                   //if (Day_Diff > 1)
                                   //{
                                          for (int i = 0; i < Day_Diff + 1; i++)
                                          //for (int i = 0; i < Day_Diff; i++)
                                          {
                                                 // DateTime d1 = fd.AddDays(i);
                                                 // var dd = d1.AddDays(i);

                                                 MooringOPRViewModel.subDates.Add(fd.AddDays(i).ToShortDateString());
                                                 OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                                          }
                                   //}
                                   //if (Day_Diff < 1)
                                   //{
                                   //       MooringOPRViewModel.subDates.Add(fd.AddDays(0).ToShortDateString());
                                   //       OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

                                   //}

                                   dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                                               dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = MooringOPRViewModel.subDates[0];

                                   cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                                   cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                                   cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                                   cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                            }
                     }
                     catch { }
              }



              private void DpCastoffDate_CalendarClosed(object sender, RoutedEventArgs e)
              {
                     try
                     {
                            var Frm = DpFastDate.Text + " " + cbHours.Text + ":" + cbMints.Text;
                            var datefrom = Convert.ToDateTime(Frm);

                            var to = DpCastoffDate.Text + " " + cbHours1.Text + ":" + cbMints1.Text;
                            var dateto = Convert.ToDateTime(to);

                            if (datefrom > dateto)
                            {
                                   MessageBox.Show("Invalid Date Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                                   DpFastDate.Text = DateTime.Now.ToShortDateString();
                                   DpCastoffDate.Text = DateTime.Now.ToShortDateString();

                                   cbHours.SelectedIndex = 0;
                                   cbMints.SelectedIndex = 0;

                                   cbHours1.SelectedIndex = 0;
                                   cbMints1.SelectedIndex = 0;

                                   cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                                   cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                                   cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                                   cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                            }
                            else
                            {

                                   DateTime cd = dateto.Date;// Convert.ToDateTime(to);
                                   DateTime fd = datefrom.Date;//Convert.ToDateTime(Frm);
                                   int Day_Diff = (int) (cd - fd).TotalDays;

                                   MooringOPRViewModel.subDates.Clear();
                                   OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));


                                   //if (Day_Diff > 1)
                                   //{
                                          for (int i = 0; i < Day_Diff + 1; i++)
                                          //for (int i = 0; i < Day_Diff; i++)
                                          {
                                                 // DateTime d1 = fd.AddDays(i);
                                                 // var dd = d1.AddDays(i);

                                                 MooringOPRViewModel.subDates.Add(fd.AddDays(i).ToShortDateString());
                                                 OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                                          }
                                   //}

                                   //if (Day_Diff <= 1)
                                   //{
                                   //       MooringOPRViewModel.subDates.Add(fd.AddDays(0).ToShortDateString());
                                   //       OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

                                   //}
                                   dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                                   dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = MooringOPRViewModel.subDates[0];

                                   cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                                   cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                                   cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                                   cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                            }

                     }
                     catch { }
              }

              //private void RaisePropertyChanged(string v)
              //{
              //       throw new NotImplementedException();
              //}

              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }

              private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
              {
                     Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
                     e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

              }

              public static bool decimalControl(char chrInput, ref TextBox txtBox, int intNoOfDec)
              {
                     /** 
                     Purpose    : To control the decimal places as per user option 
                     Assumptions: 
                     Effects    : 
                     Inputs     :  
                     Returns    : None 
                     **/
                     bool chrRetVal = false;
                     try
                     {
                            string strSearch = string.Empty;

                            if (chrInput == '\b')
                            {
                                   return false;
                            }

                            if (intNoOfDec == 0)
                            {
                                   strSearch = "0123456789";
                                   int INDEX = (int)strSearch.IndexOf(chrInput.ToString());
                                   if (strSearch.IndexOf(chrInput.ToString(), 0) == -1)
                                   {
                                          return true;
                                   }
                                   else
                                   {
                                          return false;
                                   }
                            }
                            else
                            {
                                   strSearch = "0123456789.";
                                   if (strSearch.IndexOf(chrInput, 0) == -1)
                                   {
                                          return true;
                                   }
                            }

                            if ((txtBox.Text.Length - txtBox.SelectionStart) > (intNoOfDec) && chrInput == '.')
                            {
                                   return true;
                            }

                            if (chrInput == '\b')
                            {
                                   chrRetVal = false;
                            }
                            else
                            {
                                   strSearch = txtBox.Text;
                                   if (strSearch != string.Empty)
                                   {
                                          if (strSearch.IndexOf('.', 0) > -1 && chrInput == '.')
                                          {
                                                 return true;
                                          }
                                   }
                                   int intPos;
                                   int intAftDec;

                                   strSearch = txtBox.Text;
                                   if (strSearch == string.Empty) return false;
                                   intPos = (strSearch.IndexOf('.', 0));

                                   if (intPos == -1)
                                   {
                                          strSearch = "0123456789.";
                                          if (strSearch.IndexOf(chrInput, 0) == -1)
                                          {
                                                 chrRetVal = true;
                                          }
                                          else
                                                 chrRetVal = false;
                                   }
                                   else
                                   {
                                          if (txtBox.SelectionStart > intPos)
                                          {
                                                 intAftDec = txtBox.Text.Length - txtBox.Text.IndexOf('.', 0);
                                                 if (intAftDec > intNoOfDec)
                                                 {
                                                        chrRetVal = true;
                                                 }
                                                 else
                                                        chrRetVal = false;
                                          }
                                          else
                                                 chrRetVal = false;
                                   }
                            }
                     }
                     catch (Exception ex)
                     {
                     }
                     return chrRetVal;
              }

              private void DamagedCombo_DropDownClosed(object sender, EventArgs e)
              {
                     if (DamagedCombo.Text == "No")
                     {
                            //BtnAddDamaged.Visibility = Visibility.Collapsed;
                            lblSave.Text = " Save ";
                            btnSaveAll.Width = 65;
                     }
                     else
                     {
                            //BtnAddDamaged.Visibility = Visibility.Visible;
                            lblSave.Text = " Add Damaged Line ";
                            btnSaveAll.Width = 165;
                     }

                     MooringOPRViewModel.mOperationBirths.Any_Rope_Damaged = DamagedCombo.Text;
              }

              private void CbBirthType_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.BirthType = cbBirthType.Text;
              }


              private void MooringType_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.MooringType = MooringType.Text;
              }

              private void BearthSide_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.BerthSide = BearthSide.Text;
              }

              private void CBVesselCondition_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.VesselCondition = CBVesselCondition.Text;
              }

              private void CBShipAccess_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.ShipAccess = CBShipAccess.Text;
              }

              private void CbWindDirection_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.WindDirection = CbWindDirection.Text;
              }

              private void CBSquall_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.AnySquall = CBSquall.Text;
              }

              private void CBberthExposed_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.Berth_exposed_SeaSwell = CBberthExposed.Text;
              }

              private void CBSurging_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.SurgingObserved = CBSurging.Text;
              }

              private void CBAnyAffect_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.Any_Affect_Passing_Traffic = CBAnyAffect.Text;
              }

              private void CBShipWasContact_DropDownClosed(object sender, EventArgs e)
              {
                     MooringOPRViewModel.mOperationBirths.Ship_was_continuously_contact_with_fender = CBShipWasContact.Text;
              }


              private void CbHours_DropDownClosed(object sender, EventArgs e)
              {
                     cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;

                     var Frm = DpFastDate.Text + " " + cbHours.Text + ":" + cbMints.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = DpCastoffDate.Text + " " + cbHours1.Text + ":" + cbMints1.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            DpCastoffDate.Text = DateTime.Now.ToShortDateString();
                            cbHours.SelectedIndex = 0;
                            cbMints.SelectedIndex = 0;

                            cbHours1.SelectedIndex = 0;
                            cbMints1.SelectedIndex = 0;

                            cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                            cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                            cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                            cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                     }
                     else
                     {

                            DateTime cd = Convert.ToDateTime(to);
                            DateTime fd = Convert.ToDateTime(Frm);
                            int Day_Diff = (int)(cd - fd).TotalDays;

                            MooringOPRViewModel.subDates.Clear();
                            OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

                            //if (Day_Diff > 1)
                            //{
                                   for (int i = 0; i < Day_Diff + 1; i++)
                                   //for (int i = 0; i < Day_Diff; i++)
                                   {
                                          // DateTime d1 = fd.AddDays(i);
                                          // var dd = d1.AddDays(i);

                                          MooringOPRViewModel.subDates.Add(fd.AddDays(i).ToShortDateString());
                                          OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                                   }
                            //}

                            //if (Day_Diff < 1)
                            //{
                            //       MooringOPRViewModel.subDates.Add(fd.AddDays(0).ToShortDateString());
                            //       OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                            //}
                            dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                            dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = MooringOPRViewModel.subDates[0];

                            cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                            cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                            cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                            cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                     }
              }

              private void CbMints_DropDownClosed(object sender, EventArgs e)
              {
                     cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;
              }



              private void cbHours1_DropDownClosed(object sender, EventArgs e)
              {
                     try
                     {
                            var Frm = DpFastDate.Text + " " + cbHours.Text + ":" + cbMints.Text;
                            var datefrom = Convert.ToDateTime(Frm);

                            var to = DpCastoffDate.Text + " " + cbHours1.Text + ":" + cbMints1.Text;
                            var dateto = Convert.ToDateTime(to);

                            if (datefrom > dateto)
                            {
                                   MessageBox.Show("Invalid Date Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                                   DpCastoffDate.Text = DateTime.Now.ToShortDateString();

                                   cbHours.SelectedIndex = 0;
                                   cbMints.SelectedIndex = 0;

                                   cbHours1.SelectedIndex = 0;
                                   cbMints1.SelectedIndex = 0;

                                   cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                                   cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                                   cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                                   cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                            }
                            else
                            {

                                   DateTime cd = Convert.ToDateTime(to);
                                   DateTime fd = Convert.ToDateTime(Frm);
                                   int Day_Diff = (int)(cd - fd).TotalDays;

                                   MooringOPRViewModel.subDates.Clear();
                                   OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

                                   //if (Day_Diff > 1)
                                   //{
                                         for (int i = 0; i < Day_Diff + 1; i++)
                                          //for (int i = 0; i < Day_Diff; i++)
                                          {
                                                 // DateTime d1 = fd.AddDays(i);
                                                 // var dd = d1.AddDays(i);

                                                 MooringOPRViewModel.subDates.Add(fd.AddDays(i).ToShortDateString());
                                                 OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                                          }
                                   //}

                                   //if (Day_Diff <= 1)
                                   //{
                                   //       MooringOPRViewModel.subDates.Add(fd.AddDays(0).ToShortDateString());
                                   //       OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                                   //}

                                   dpSubFastGrid1.Text = dpSubCastGrid1.Text = dpSubFastGrid2.Text = dpSubCastGrid2.Text =
                                               dpSubFastGrid3.Text = dpSubCastGrid3.Text = dpSubFastGrid4.Text = dpSubCastGrid4.Text = MooringOPRViewModel.subDates[0];

                                   cbStartHours1.Text = cbStartHours2.Text = cbStartHours3.Text = cbStartHours4.Text = cbHours.Text;
                                   cbStartMint1.Text = cbStartMint2.Text = cbStartMint3.Text = cbStartMint4.Text = cbMints.Text;

                                   cbEndHours1.Text = cbEndHours2.Text = cbEndHours3.Text = cbEndHours4.Text = cbHours1.Text;
                                   cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
                            }
                     }
                     catch { }
              }

              private void txtArrivalFWD_PreviewTextInput(object sender, TextCompositionEventArgs e)
              {

              }

              private void txtArrivalFWD_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
              {

              }

              private void TxtArrivalFWD_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            decimal checkvalue = 0;
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(TxtArrivalFWD.Text));
                            if (check == false)
                            {
                                   TxtArrivalFWD.Text = "";
                            }
                            else
                            {
                                   checkvalue = Convert.ToDecimal(TxtArrivalFWD.Text);
                            }

                            decimal depth = 0;
                            if (!string.IsNullOrEmpty(TxtDepthAtBerth.Text))
                            {
                                   depth = Convert.ToDecimal(TxtDepthAtBerth.Text);
                            }

                            if (depth != 0)
                            {
                                   if (depth <= checkvalue)
                                   {
                                          MessageBox.Show("Depth at Berth cannot be less than Maximum draft", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          TxtDepthAtBerth.Text = "";
                                   }
                            }
                     }
                     catch { }
              }

              private void TxtArrivalAFT_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            decimal checkvalue = 0;
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(TxtArrivalAFT.Text));
                            if (check == false)
                            {
                                   TxtArrivalAFT.Text = "";
                            }
                            else
                            {
                                   checkvalue = Convert.ToDecimal(TxtArrivalAFT.Text);
                            }

                            decimal depth = 0;
                            if (!string.IsNullOrEmpty(TxtDepthAtBerth.Text))
                            {
                                   depth = Convert.ToDecimal(TxtDepthAtBerth.Text);
                            }

                            if (depth != 0)
                            {
                                   if (depth <= checkvalue)
                                   {
                                          MessageBox.Show("Depth at Berth cannot be less than Maximum draft", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          TxtDepthAtBerth.Text = "";
                                   }
                            }
                     }
                     catch { }
              }

              private void TxtDepartureFWD_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            decimal checkvalue = 0;
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(TxtDepartureFWD.Text));
                            if (check == false)
                            {
                                   TxtDepartureFWD.Text = "";
                            }
                            else
                            {
                                   checkvalue = Convert.ToDecimal(TxtDepartureFWD.Text);
                            }

                            decimal depth = 0;
                            if (!string.IsNullOrEmpty(TxtDepthAtBerth.Text))
                            {
                                   depth = Convert.ToDecimal(TxtDepthAtBerth.Text);
                            }

                            if (depth != 0)
                            {
                                   if (depth <= checkvalue)
                                   {
                                          MessageBox.Show("Depth at Berth cannot be less than Maximum draft", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          TxtDepthAtBerth.Text = "";
                                   }
                            }
                     }
                     catch { }
              }

              private void TxtDepartureAFT_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            decimal checkvalue = 0;
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(TxtDepartureAFT.Text));
                            if (check == false)
                            {
                                   TxtDepartureAFT.Text = "";
                            }
                            else
                            {
                                   checkvalue = Convert.ToDecimal(TxtDepartureAFT.Text);
                            }
                            decimal depth = 0;
                            if (!string.IsNullOrEmpty(TxtDepthAtBerth.Text))
                            {
                                   depth = Convert.ToDecimal(TxtDepthAtBerth.Text);
                            }

                            if (depth != 0)
                            {
                                   if (depth <= checkvalue)
                                   {
                                          MessageBox.Show("Depth at Berth cannot be less than Maximum draft", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          TxtDepthAtBerth.Text = "";
                                   }
                            }
                     }
                     catch { }
              }

              private void TxtDepthAtBerth_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            bool check = sc.DecimalCheck1(Convert.ToDecimal(TxtDepthAtBerth.Text));
                            if (check == false)
                            {
                                   TxtDepthAtBerth.Text = "";
                            }

                            


                     }
                     catch (Exception ex) { }
              }

              private void TxtRangeTide_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(TxtRangeTide.Text));
                            if (check == false)
                            {
                                   TxtRangeTide.Text = "";
                            }
                     }
                     catch { }
              }

              private void TxtCurrentSpeed_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(TxtCurrentSpeed.Text));
                            if (check == false)
                            {
                                   TxtCurrentSpeed.Text = "";
                            }
                     }
                     catch { }
              }

              private void txtInspectBy_PreviewKeyDown(object sender, KeyEventArgs e)
              {
                     if (e.Key == Key.Down)
                     {
                            lbSuggestion.Focus();
                     }
              }

              private void lbSuggestion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
              {
                     if (lbSuggestion.SelectedIndex > -1)
                     {

                            txtInspectBy.Text = lbSuggestion.SelectedItem.ToString();

                            if (txtInspectBy.Text == "Others")
                            {
                                   //txtfac.Visibility = Visibility.Collapsed;
                                   //txtoth.Visibility = Visibility.Visible;
                                   txtportname.Visibility = Visibility.Visible;
                                   txtprtnm.Visibility = Visibility.Visible;

                                   txtFacilityName.SelectedIndex = 0;
                                   txtFacilityName.IsEnabled = false;
                                   txtOthers.Visibility = Visibility.Visible; 
                                   txtOthers.Text = "";
                                   MooringOPRViewModel.mOperationBirths.FacilityName = null;
                            }
                            else
                            {
                                   //txtfac.Visibility = Visibility.Visible;
                                   //txtoth.Visibility = Visibility.Collapsed;

                                   txtFacilityName.IsEnabled = true;
                                   txtOthers.Visibility = Visibility.Hidden;
                                   txtportname.Visibility = Visibility.Collapsed;
                                   txtprtnm.Visibility = Visibility.Collapsed;
                            }

                            lbSuggestion.Visibility = Visibility.Collapsed;

                     }
              }

              private void lbSuggestion_KeyDown(object sender, KeyEventArgs e)
              {
                     if (lbSuggestion.SelectedIndex > -1)
                     {
                            if (e.Key == Key.Enter)
                            {
                                   txtInspectBy.Text = lbSuggestion.SelectedItem.ToString();
                                   lbSuggestion.Visibility = Visibility.Collapsed;
                            }


                     }
              }

              private void txtportname_MouseLeave(object sender, MouseEventArgs e)
              {
                     try
                     {
                            MooringOPRViewModel.mOperationBirths.PortName = txtportname.Text;
                     }
                     catch { }
              }




              int check = 0; int msgchk = 0;
              private void txtInspectBy_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            var ss = txtInspectBy.Text;

                            check = 0;
                            var PortNames = new ObservableCollection<string>();
                            var data = sc.PortListtbl.Where(x => x.PortName.ToLower().Contains(ss.Trim().ToLower())).Select(x => new { x.PortName }).ToList().Distinct();

                            foreach (var item in data)
                            {
                                   PortNames.Add(item.PortName);
                                   check = 1;
                                   msgchk = 0;
                            }
                            if (check == 0)
                            {
                                   if (msgchk == 0)
                                   {
                                          if (ss != "")
                                          {
                                                 MessageBox.Show("Port Name does not exist in the list, please search and select name from the list !", "Wrong Input", MessageBoxButton.OK, MessageBoxImage.Warning);

                                                 check = 1;
                                                 msgchk = 1;
                                                 txtInspectBy.Text = string.Empty;
                                          }
                                   }
                            }
                     }
                     catch { }

              }



              private void lbSuggestion_MouseUp(object sender, MouseButtonEventArgs e)
              {
                     if (lbSuggestion.SelectedIndex > -1)
                     {

                            txtInspectBy.Text = lbSuggestion.SelectedItem.ToString();

                            if (txtInspectBy.Text == "Others")
                            {
                                   //txtfac.Visibility = Visibility.Collapsed;
                                   //txtoth.Visibility = Visibility.Visible;
                                   txtportname.Visibility = Visibility.Visible;
                                   txtprtnm.Visibility = Visibility.Visible;

                                   txtFacilityName.SelectedIndex = 0;
                                   txtFacilityName.IsEnabled = false;
                                   txtOthers.Visibility = Visibility.Visible;
                                   txtOthers.Text = "";
                                   MooringOPRViewModel.mOperationBirths.FacilityName = null;
                            }
                            else
                            {
                                   //txtfac.Visibility = Visibility.Visible;
                                   //txtoth.Visibility = Visibility.Collapsed;
                                   txtFacilityName.IsEnabled = true;
                                   txtOthers.Visibility = Visibility.Hidden;
                                   txtportname.Visibility = Visibility.Collapsed;
                                   txtprtnm.Visibility = Visibility.Collapsed;
                            }

                            lbSuggestion.Visibility = Visibility.Collapsed;

                     }
              }

              private void txtAirTemp_TextChanged(object sender, TextChangedEventArgs e)
              {
                     try
                     {
                            bool check = sc.DecimalCheck2Digit(Convert.ToInt32(txtAirTemp.Text));
                            if (check == false)
                            {
                                   txtAirTemp.Text = "";
                            }
                     }
                     catch { }
              }


              private void cbStartHours1_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid1.Text + " " + cbStartHours1.Text + ":" + cbStartMint1.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid1.Text + " " + cbEndHours1.Text + ":" + cbEndMint1.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            dpSubFastGrid1.SelectedIndex = 0;
                            cbStartHours1.SelectedIndex = 0;
                            cbStartMint1.SelectedIndex = 0;

                            dpSubCastGrid1.SelectedIndex = 0;
                            cbEndHours1.SelectedIndex = 0;
                            cbEndMint1.SelectedIndex = 0;
                     }


              }

              private void cbEndHours1_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid1.Text + " " + cbStartHours1.Text + ":" + cbStartMint1.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid1.Text + " " + cbEndHours1.Text + ":" + cbEndMint1.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);

                            dpSubFastGrid1.SelectedIndex = 0;
                            cbStartHours1.SelectedIndex = 0;
                            cbStartMint1.SelectedIndex = 0;

                            dpSubCastGrid1.SelectedIndex = 0;
                            cbEndHours1.SelectedIndex = 0;
                            cbEndMint1.SelectedIndex = 0;
                     }
              }

              private void dpSubFastGrid2_DropDownClosed(object sender, EventArgs e)
              {

                     var Frm = dpSubFastGrid2.Text + " " + cbStartHours2.Text + ":" + cbStartMint2.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid2.Text + " " + cbEndHours2.Text + ":" + cbEndMint2.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);

                            dpSubFastGrid2.SelectedIndex = 0;
                            cbStartHours2.SelectedIndex = 0;
                            cbStartMint2.SelectedIndex = 0;

                            dpSubCastGrid2.SelectedIndex = 0;
                            cbEndHours2.SelectedIndex = 0;
                            cbEndMint2.SelectedIndex = 0;
                     }

              }

              private void dpSubCastGrid2_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid2.Text + " " + cbStartHours2.Text + ":" + cbStartMint2.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid2.Text + " " + cbEndHours2.Text + ":" + cbEndMint2.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            dpSubFastGrid2.SelectedIndex = 0;
                            cbStartHours2.SelectedIndex = 0;
                            cbStartMint2.SelectedIndex = 0;

                            dpSubCastGrid2.SelectedIndex = 0;
                            cbEndHours2.SelectedIndex = 0;
                            cbEndMint2.SelectedIndex = 0;
                     }
              }

              private void dpSubFastGrid3_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid3.Text + " " + cbStartHours3.Text + ":" + cbStartMint3.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid3.Text + " " + cbEndHours3.Text + ":" + cbEndMint3.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);

                            dpSubFastGrid3.SelectedIndex = 0;
                            cbStartHours3.SelectedIndex = 0;
                            cbStartMint3.SelectedIndex = 0;

                            dpSubCastGrid3.SelectedIndex = 0;
                            cbEndHours3.SelectedIndex = 0;
                            cbEndMint3.SelectedIndex = 0;
                     }
              }

              private void dpSubCastGrid3_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid3.Text + " " + cbStartHours3.Text + ":" + cbStartMint3.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid3.Text + " " + cbEndHours3.Text + ":" + cbEndMint3.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            dpSubFastGrid3.SelectedIndex = 0;
                            cbStartHours3.SelectedIndex = 0;
                            cbStartMint3.SelectedIndex = 0;

                            dpSubCastGrid3.SelectedIndex = 0;
                            cbEndHours3.SelectedIndex = 0;
                            cbEndMint3.SelectedIndex = 0;
                     }
              }

              private void dpSubFastGrid4_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid4.Text + " " + cbStartHours4.Text + ":" + cbStartMint4.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid4.Text + " " + cbEndHours4.Text + ":" + cbEndMint4.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);

                            dpSubFastGrid4.SelectedIndex = 0;
                            cbStartHours4.SelectedIndex = 0;
                            cbStartMint4.SelectedIndex = 0;

                            dpSubCastGrid4.SelectedIndex = 0;
                            cbEndHours4.SelectedIndex = 0;
                            cbEndMint4.SelectedIndex = 0;
                     }
              }

              private void dpSubCastGrid4_DropDownClosed(object sender, EventArgs e)
              {
                     var Frm = dpSubFastGrid4.Text + " " + cbStartHours4.Text + ":" + cbStartMint4.Text;
                     var datefrom = Convert.ToDateTime(Frm);

                     var to = dpSubCastGrid4.Text + " " + cbEndHours4.Text + ":" + cbEndMint4.Text;
                     var dateto = Convert.ToDateTime(to);

                     if (datefrom > dateto)
                     {
                            MessageBox.Show("Invalid Date or Time Selected!", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            dpSubFastGrid4.SelectedIndex = 0;
                            cbStartHours4.SelectedIndex = 0;
                            cbStartMint4.SelectedIndex = 0;

                            dpSubCastGrid4.SelectedIndex = 0;
                            cbEndHours4.SelectedIndex = 0;
                            cbEndMint4.SelectedIndex = 0;
                     }
              }

              private void TxtDepthAtBerth_LostFocus(object sender, RoutedEventArgs e)
              {
                     //if (depthatberth == 1)
                     //{
                     //       TxtDepthAtBerth.Text = "";
                     //}

                     decimal depth = 0;
                     if (!string.IsNullOrEmpty(TxtDepthAtBerth.Text))
                     {
                            depth = Convert.ToDecimal(TxtDepthAtBerth.Text);
                     }

                     decimal first = 0; decimal second = 0; decimal third = 0; decimal fourth = 0;


                     decimal maxvalu = 0;
                     if (!string.IsNullOrEmpty(TxtArrivalAFT.Text))
                     {
                            first = Convert.ToDecimal(TxtArrivalAFT.Text);
                            maxvalu = first;
                     }
                     if (!string.IsNullOrEmpty(TxtArrivalFWD.Text))
                     {
                            second = Convert.ToDecimal(TxtArrivalFWD.Text);
                            maxvalu = second;
                     }
                     if (!string.IsNullOrEmpty(TxtDepartureAFT.Text))
                     {

                            third = Convert.ToDecimal(TxtDepartureAFT.Text);
                            maxvalu = third;
                     }
                     if (!string.IsNullOrEmpty(TxtDepartureFWD.Text))
                     {
                            fourth = Convert.ToDecimal(TxtDepartureFWD.Text);
                            maxvalu = fourth;
                     }


                     if ((first > second) && (first > third) && (first > fourth))
                     {
                            maxvalu = first;
                     }
                     if ((second > first) && (second > third) && (second > fourth))
                     {
                            maxvalu = second;
                     }
                     if ((third > first) && (third > second) && (third > fourth))
                     {
                            maxvalu = third;
                     }
                     if ((fourth > first) && (fourth > second) && (fourth > third))
                     {
                            maxvalu = fourth;
                     }

                     depthatberth = 0;

                     if (depth <= maxvalu)
                     {
                            MessageBox.Show("Depth at Berth cannot be less than Maximum draft", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                            TxtDepthAtBerth.Text = "";
                            //depthatberth = 1;
                     }


              }

              private void CBPosNeg_DropDownClosed(object sender, EventArgs e)
              {
                     try
                     {
                            string posneg = CBPosNeg.Text;

                            if (posneg == "Positive")
                            {
                                   MooringOPRViewModel.mOperationBirths.AirTempCentigrate = true;
                            }
                            if (posneg == "Negative")
                            {
                                   MooringOPRViewModel.mOperationBirths.AirTempCentigrate = false;
                            }
                     }
                     catch { }

                     //MooringOPRViewModel.mOperationBirths.SurgingObserved = CBPosNeg.Text;
              }

              private void TxtFacilityName_DropDownClosed(object sender, EventArgs e)
              {
                     try
                     {
                            if (txtFacilityName.Text == "Other")
                            {
                                   //txtfac.Visibility = Visibility.Collapsed;
                                   txtOthers.Visibility = Visibility.Visible;
                                   txtOthers.Text = "";
                                   MooringOPRViewModel.mOperationBirths.FacilityName = null;
                            }
                            else
                            {
                                   //txtfac.Visibility = Visibility.Visible;
                                   txtOthers.Visibility = Visibility.Hidden;

                            }

                     }
                     catch { }
              }


              private void ComboBox_Loaded(object sender, RoutedEventArgs e)
              {
                     ObservableCollection<Programs> data = new ObservableCollection<Programs>();
                     data.Add(new Programs("test", false));
                     data.Add(new Programs("test1", false));
                     data.Add(new Programs("test2", true));

                     SqlDataAdapter adp = new SqlDataAdapter("Mop_AssignRopteTailBind", sc.con);
                     adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                     DataTable dt = new DataTable();
                     adp.Fill(dt);

                     // ... Get the ComboBox reference.  
                     var comboBox = sender as ComboBox;

                     // ... Assign the ItemsSource to the List.  
                     // comboBox.ItemsSource = data;

                     comboBox.ItemsSource = dt.Rows;

                     // ... Make the first item selected.  
                     comboBox.SelectedIndex = 0;
              }

              public class Programs
              {
                     public Programs(string Program, bool IsChecked)
                     {
                            this.Program = Program;
                            this.IsChecked = IsChecked;
                     }
                     public string Program { get; set; }
                     public bool IsChecked { get; set; }
              }

              private void cmbPayee_Loaded(object sender, RoutedEventArgs e)
              {
                     ComboBox cmb = (ComboBox)sender;
                     SqlDataAdapter adp = new SqlDataAdapter("Mop_AssignRopteTailBind", sc.con);
                     adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                     DataTable dt = new DataTable();
                     adp.Fill(dt);

                     cmb.ItemsSource = dt.Rows;
                     cmb.DisplayMemberPath = "RopeId";
                     cmb.SelectedValuePath = "RopeId";
              }

              private void CbMints1_DropDownClosed(object sender, EventArgs e)
              {
                     cbEndMint1.Text = cbEndMint2.Text = cbEndMint3.Text = cbEndMint4.Text = cbMints1.Text;
              }
       }
}
