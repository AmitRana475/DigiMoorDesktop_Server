using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using DataBuildingLayer;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for RopeDetailListView.xaml
    /// </summary>
    public partial class RopeDetailListView : UserControl
    {
        private readonly ShipmentContaxt sc;


        public RopeDetailListView()
        {
            try
            {
                InitializeComponent();

                if (sc == null)
                {
                    sc = new ShipmentContaxt();
                }
                int ropeid = StaticHelper.RopeId_Status;

                            if(StaticHelper.RopeTail==1)
                            {
                                   B3.Visibility = Visibility.Hidden;
                    B9.Visibility = Visibility.Hidden;

                }
                            else
                            {
                                   B3.Visibility = Visibility.Visible;
                    B3.Visibility = Visibility.Visible;

                }

                GetRopeDetailList(ropeid);


                //SqlDataAdapter adp = new SqlDataAdapter("Select  c.location, c.assignednumber,d.currentrunninghours,d.MaxRunningHours,InspectionDueDate from AssignRopeToWinch b inner join MooringWinchDetail c on b.WinchId=c.Id inner join MooringRopeDetail d on b.RopeId=d.Id where b.RopeId=" + ropeid + "", sc.con);

                //SqlDataAdapter adp = new SqlDataAdapter("Select  c.location, c.assignednumber,d.currentrunninghours,(select k.Maximumrunninghours from tblRopeInspectionSetting k where d.ropetypeid=k.MooringRopeType and d.ManufacturerId=k.ManufacturerType) as MaxRunningHours,InspectionDueDate from AssignRopeToWinch b inner join MooringWinchDetail c on b.WinchId=c.Id inner join MooringRopeDetail d on b.RopeId=d.Id where b.RopeId=" + ropeid + "", sc.con);

                SqlDataAdapter adp = new SqlDataAdapter("RopeDetailform", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure; ;
                adp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAssignWinch.Text = "[Winch - " + dt.Rows[0]["assignednumber"].ToString() + "] ";
                    txtlocation.Text = "[Location - " + dt.Rows[0]["location"].ToString() + "] ";
                    txtRuninghrs.Text = "[Total Running Hours - " + dt.Rows[0]["currentrunninghours"].ToString() + "] ";

                    int rptl = Convert.ToInt32(dt.Rows[0]["ropetail"]);
                    if (rptl == 0)
                    {
                        txtcrntoutboard.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        txtcrntoutboard.Visibility = Visibility.Hidden;
                    }

                    txtcrntoutboard.Text = "[OutboardEnd -  " + dt.Rows[0]["OutboardEnd"].ToString() + "] ";
                    //txtMaxRuninghrs.Text = " [MaxRunHrs - " + dt.Rows[0]["MaxRunningHours"].ToString() + "] ";



                    //string ss = Convert.ToDateTime(dt.Rows[0]["InspectionDueDate"]).ToString("dd-MMM-yyyy");
                    //txtNxtInspDue.Text =  "[Next Inspection Due - "+ ss +"] ";

                }
                else
                {
                    txtcrntoutboard.Text = "[OutboardEnd -  Not Assigned ] ";
                    txtAssignWinch.Text = "[Winch -  Not Assigned ] ";
                    txtlocation.Text = "[Location - Not Assigned] ";
                }
                try
                {
                    SqlDataAdapter adp1 = new SqlDataAdapter("select InspectionDueDate,CurrentRunningHours from MooringRopeDetail where ID= " + ropeid + "", sc.con);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    if(dt1.Rows.Count>0)
                    {
                        var rnhrs = Convert.ToString(dt1.Rows[0]["CurrentRunningHours"] == DBNull.Value ? "Not Assigned": dt1.Rows[0]["CurrentRunningHours"]).ToString();


                        txtRuninghrs.Text = " [Total Running Hours - " + rnhrs + "] ";

                        string ss = Convert.ToDateTime(dt1.Rows[0]["InspectionDueDate"] == DBNull.Value ? null : dt1.Rows[0]["InspectionDueDate"]).ToString("dd-MMM-yyyy");

                        if (ss == "01-Jan-0001")
                        {
                            ss = "Not Assigned";
                        }
                        txtNxtInspDue.Text = "[Next Inspection Due - " + ss + "] ";
                    }
                }
                catch (Exception ex) { }

                B1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B1.BorderThickness = new Thickness(2);
                B1.Background = Brushes.SkyBlue;

                
                //------------------------------------
                B2.Background = Brushes.LightGray;
                B3.Background = Brushes.LightGray;
                B4.Background = Brushes.LightGray;
                B5.Background = Brushes.LightGray;
                B6.Background = Brushes.LightGray;
                B7.Background = Brushes.LightGray;
                B8.Background = Brushes.LightGray;
                B9.Background = Brushes.LightGray;
            }
            catch(Exception ex) { }
        }

        private void GetRopeDetailList(int ropeid)
        {
            try
  
          {
   if(StaticHelper.RopeTail==1)
                            {
                                   B1.Content = " Tail Detail ";
                            }
                            if (StaticHelper.RopeTail == 0)
                            {
                                   B1.Content = " Line Detail ";
                            }

                            SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", ropeid);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "RopeDetail");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropedetailGrid.ItemsSource = ds.Tables[0].DefaultView;

                                   if(StaticHelper.RopeTail==1)
                                   {
                                          ropedetailGrid.Columns[9].Header = "TDBF";
                                   }
                                   if (StaticHelper.RopeTail == 0)
                                   {
                                          ropedetailGrid.Columns[9].Header = "LDBF";
                                   }
                                  
                                   lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) { }
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            B1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B1.BorderThickness = new Thickness(2);
            B1.Background = Brushes.SkyBlue;

            B2.BorderThickness = new Thickness(0);
            B2.Background = Brushes.LightGray;

            B3.BorderThickness = new Thickness(0);
            B3.Background = Brushes.LightGray;

            B4.BorderThickness = new Thickness(0);
            B4.Background = Brushes.LightGray;

            B9.BorderThickness = new Thickness(0);
            B9.Background = Brushes.LightGray;

            B5.BorderThickness = new Thickness(0);
            B5.Background = Brushes.LightGray;

            B6.BorderThickness = new Thickness(0);
            B6.Background = Brushes.LightGray;

            B7.BorderThickness = new Thickness(0);
            B7.Background = Brushes.LightGray;

            B8.BorderThickness = new Thickness(0);
            B8.Background = Brushes.LightGray;

            ropedetailGrid.Visibility = Visibility.Visible;
            lblError.Visibility = Visibility.Hidden;
            ropecroppingGrid.Visibility = Visibility.Collapsed;
            ropesplicingGrid.Visibility = Visibility.Collapsed;
            ropediscardGrid.Visibility = Visibility.Collapsed;
            ropedamageGrid.Visibility = Visibility.Collapsed;
            ropeendtoendGrid.Visibility = Visibility.Collapsed;
            ropedisposalGrid.Visibility = Visibility.Collapsed;
            ropeinspectionGrid.Visibility = Visibility.Collapsed;
            winchrotationGrid.Visibility = Visibility.Collapsed;
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B2.BorderThickness = new Thickness(2);
                B2.Background = Brushes.SkyBlue;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "RopeInspection");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropeinspectionGrid.ItemsSource = ds.Tables[0].DefaultView;


                                   if (StaticHelper.RopeTail == 1)
                                   {
                                          ropeinspectionGrid.MaxWidth = 2100;
                                          ropeinspectionGrid.MinWidth = 2000;
                                          ropeinspectionGrid.Columns[13].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[14].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[15].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[16].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[17].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[18].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[19].Visibility = Visibility.Hidden;
                                   }
                                   if (StaticHelper.RopeTail == 0)
                                   {
                                          
                                          ropeinspectionGrid.MaxWidth = 3100;
                                          ropeinspectionGrid.MinWidth = 3080;
                                          ropeinspectionGrid.Columns[13].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[14].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[15].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[16].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[17].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[18].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[19].Visibility = Visibility.Visible;
                                   }

                                   lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                                   if (StaticHelper.RopeTail == 1)
                                   {
                                          ropeinspectionGrid.MaxWidth = 2100;
                                          ropeinspectionGrid.MinWidth = 2000;
                                          ropeinspectionGrid.Columns[13].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[14].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[15].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[16].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[17].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[18].Visibility = Visibility.Hidden;
                                          ropeinspectionGrid.Columns[19].Visibility = Visibility.Hidden;
                                   }
                                   if (StaticHelper.RopeTail == 0)
                                   {


                                          ropeinspectionGrid.MaxWidth = 3100;
                                          ropeinspectionGrid.MinWidth = 3080;


                                          ropeinspectionGrid.Columns[13].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[14].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[15].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[16].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[17].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[18].Visibility = Visibility.Visible;
                                          ropeinspectionGrid.Columns[19].Visibility = Visibility.Visible;
                                   }

                                   lblError.Visibility = Visibility.Visible;
                }
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Visible;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B3.BorderThickness = new Thickness(2);
                B3.Background = Brushes.SkyBlue;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "EndtoEnd");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropeendtoendGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Visible;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B4.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B4.BorderThickness = new Thickness(2);
                B4.Background = Brushes.SkyBlue;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;


                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "Splicing");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropesplicingGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Visible;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void B5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B5.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B5.BorderThickness = new Thickness(2);
                B5.Background = Brushes.SkyBlue;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "Cropping");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropecroppingGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Visible;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void B6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B6.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B6.BorderThickness = new Thickness(2);
                B6.Background = Brushes.SkyBlue;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "Damage");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropedamageGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Visible;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void B7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B7.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B7.BorderThickness = new Thickness(2);
                B7.Background = Brushes.SkyBlue;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;


                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "Discard");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropediscardGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                ropediscardGrid.Visibility = Visibility.Visible;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void B8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B8.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B8.BorderThickness = new Thickness(2);
                B8.Background = Brushes.SkyBlue;

                B9.BorderThickness = new Thickness(0);
                B9.Background = Brushes.LightGray;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "Disposal");
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTail);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ropedisposalGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }
               
                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropedisposalGrid.Visibility = Visibility.Visible;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;
                winchrotationGrid.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object ID = ((Button)sender).CommandParameter;
                int id = Convert.ToInt32(ID);
                StaticHelper.ViewId = id;
                StaticHelper.Photos = "Image1";
                            StaticHelper.TbName = "MooringRopeInspection";
                            ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                object ID = ((Button)sender).CommandParameter;
                int id = Convert.ToInt32(ID);
                StaticHelper.ViewId = id;
                StaticHelper.Photos = "Image2";
                            StaticHelper.TbName = "MooringRopeInspection";
                            ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());
            }
            catch { }
        }

        private void B9_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B9.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                B9.BorderThickness = new Thickness(2);
                B9.Background = Brushes.SkyBlue;


                B8.BorderThickness = new Thickness(0);
                B8.Background = Brushes.LightGray;

                B1.BorderThickness = new Thickness(0);
                B1.Background = Brushes.LightGray;

                B3.BorderThickness = new Thickness(0);
                B3.Background = Brushes.LightGray;

                B4.BorderThickness = new Thickness(0);
                B4.Background = Brushes.LightGray;

                B5.BorderThickness = new Thickness(0);
                B5.Background = Brushes.LightGray;

                B6.BorderThickness = new Thickness(0);
                B6.Background = Brushes.LightGray;

                B7.BorderThickness = new Thickness(0);
                B7.Background = Brushes.LightGray;

                B2.BorderThickness = new Thickness(0);
                B2.Background = Brushes.LightGray;

                string qry = @"select case when a.assignednumber is null  then 'UnAssigned'when a.assignednumber is not null then a.assignednumber end as AssignedNumber,case when a.Location is null  then 'UnAssigned' when a.Location is not null then a.Location end as Location,
case when a.Lead is null  then 'UnAssigned' when a.Lead is not null then a.Lead
end as Lead,b.AssignedDate, b.RunningHours from MooringWinchDetail a  right join 
WinchRotation b on a.Id=b.WinchId where ropeid=" + StaticHelper.RopeId_Status + "";

                SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                //SqlDataAdapter adp = new SqlDataAdapter("GetRopeStatus", sc.con);
                //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                //adp.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.RopeId_Status);
                //adp.SelectCommand.Parameters.AddWithValue("@Action", "Other");
                //adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    winchrotationGrid.ItemsSource = ds.Tables[0].DefaultView;
                    lblError.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblError.Visibility = Visibility.Visible;
                }

                ropedetailGrid.Visibility = Visibility.Collapsed;
                ropedisposalGrid.Visibility = Visibility.Collapsed;
                ropediscardGrid.Visibility = Visibility.Collapsed;
                ropecroppingGrid.Visibility = Visibility.Collapsed;
                ropedamageGrid.Visibility = Visibility.Collapsed;
                ropesplicingGrid.Visibility = Visibility.Collapsed;
                ropeinspectionGrid.Visibility = Visibility.Collapsed;
                ropeendtoendGrid.Visibility = Visibility.Collapsed;

                winchrotationGrid.Visibility = Visibility.Visible;
            }
            catch { }
        }
    }
}
