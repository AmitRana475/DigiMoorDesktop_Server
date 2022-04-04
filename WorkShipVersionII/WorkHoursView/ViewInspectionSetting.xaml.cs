using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WorkShipVersionII.WorkHoursView
{
       /// <summary>
       /// Interaction logic for ViewInspectionSetting.xaml
       /// </summary>
       public partial class ViewInspectionSetting : UserControl
       {
              private readonly ShipmentContaxt sc;
              public ViewInspectionSetting()
              {
                     InitializeComponent();
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                     }
                     // bindLooseEType();

                     bindRopeDetail();
              }

              private void bindLooseEType()
              {
                     try
                     {
                            SqlDataAdapter adp1 = new SqlDataAdapter("select * from looseetype", sc.con);
                            DataSet dt1 = new DataSet();
                            adp1.Fill(dt1);
                            if (dt1.Tables[0].Rows.Count > 0)
                            {

                                   comboLooseEtype.ItemsSource = dt1.Tables[0].DefaultView;
                                   comboLooseEtype.DisplayMemberPath = dt1.Tables[0].Columns["LooseEquipmentType"].ToString();
                                   comboLooseEtype.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();

                            }
                     }
                     catch { }
              }

              private void bindRopeDetail()
              {
                     try
                     {
                            SqlDataAdapter adp1 = new SqlDataAdapter("VeiwRopeInspectionSetting", sc.con);
                            adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                            //adp1.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                            DataSet dt1 = new DataSet();
                            adp1.Fill(dt1);
                            if (dt1.Tables[0].Rows.Count > 0)
                            {
                                   mooringropeSe.ItemsSource = dt1.Tables[0].DefaultView;
                            }

                     }
                     catch { }
              }
              private void bindLooseEInsSetting()
              {
                     try
                     {
                            SqlDataAdapter adp1 = new SqlDataAdapter("select  a.*,b.looseequipmenttype from tblLooseEquipInspectionSetting a inner join looseetype b on a.EquipmentType=b.Id", sc.con);
                            DataSet dt1 = new DataSet();
                            adp1.Fill(dt1);
                            if (dt1.Tables[0].Rows.Count > 0)
                            {
                                   MooringLooseEInspectionGrid.ItemsSource = dt1.Tables[0].DefaultView;
                            }

                     }
                     catch { }
              }

              private void CBShipWasContact_DropDownClosed(object sender, EventArgs e)
              {
                     try
                     {
                            if (CBShipWasContact.Text != "--Select--")
                            {
                                   mooringropeSe.Visibility = Visibility.Visible;
                                   MooringLooseEInspectionGrid.Visibility = Visibility.Collapsed;

                                   if (CBShipWasContact.Text == "Rope Tail")
                                   {
                                          mooringropeSe1.Visibility = Visibility.Visible;
                                          mooringropeSe.Visibility = Visibility.Hidden;

                                          SqlDataAdapter adp1 = new SqlDataAdapter("VeiwRopeInspectionSetting", sc.con);
                                          adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                                          //adp1.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                                          DataSet dt1 = new DataSet();
                                          adp1.Fill(dt1);
                                          if (dt1.Tables[1].Rows.Count > 0)
                                          {
                                                 mooringropeSe1.ItemsSource = dt1.Tables[1].DefaultView;
                                          }
                                          else
                                          {
                                                 mooringropeSe1.ItemsSource = null;
                                          }
                                   }
                                   if (CBShipWasContact.Text == "Line")
                                   {
                                          mooringropeSe1.Visibility = Visibility.Hidden;
                                          mooringropeSe.Visibility = Visibility.Visible;
                                          SqlDataAdapter adp1 = new SqlDataAdapter("VeiwRopeInspectionSetting", sc.con);
                                          adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                                          //adp1.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                                          DataSet dt1 = new DataSet();
                                          adp1.Fill(dt1);
                                          if (dt1.Tables[0].Rows.Count > 0)
                                          {
                                                 mooringropeSe.ItemsSource = dt1.Tables[0].DefaultView;
                                          }
                                          else
                                          {
                                                 mooringropeSe.ItemsSource = null;
                                          }
                                   }
                                                                     
                            }
                     }
                     catch { }
              }

              private void comboLooseEtype_DropDownClosed(object sender, EventArgs e)
              {
                     try
                     {
                            if (comboLooseEtype.Text != "--Select--")
                            {
                                   mooringropeSe.Visibility = Visibility.Collapsed;
                                   MooringLooseEInspectionGrid.Visibility = Visibility.Visible;

                                   var id = comboLooseEtype.SelectedValue;

                                   SqlDataAdapter adp1 = new SqlDataAdapter("select  a.*,b.looseequipmenttype from tblLooseEquipInspectionSetting a inner join looseetype b on a.EquipmentType=b.Id where a.EquipmentType=" + id + "", sc.con);
                                   DataSet dt1 = new DataSet();
                                   adp1.Fill(dt1);
                                   if (dt1.Tables[0].Rows.Count > 0)
                                   {
                                          MooringLooseEInspectionGrid.ItemsSource = dt1.Tables[0].DefaultView;
                                   }
                                   else
                                   {
                                          MooringLooseEInspectionGrid.ItemsSource = null;
                                   }
                            }
                     }
                     catch { }
              }
       }
}
