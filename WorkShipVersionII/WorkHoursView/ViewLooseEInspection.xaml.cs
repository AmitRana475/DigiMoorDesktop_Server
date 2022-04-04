﻿using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for ViewLooseEInspection.xaml
    /// </summary>
    public partial class ViewLooseEInspection : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewLooseEInspection()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
          
            SqlDataAdapter adp = new SqlDataAdapter("select a.*,b.looseequipmenttype from MooringLooseEquipInspection a inner join LooseEType b on a.LooseETypeId=b.Id where a.id=" + id + "", sc.con);
       
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txt2.Text = dt.Rows[0]["InspectBy"] == DBNull.Value ? "-" : dt.Rows[0]["InspectBy"].ToString();

                txt3.Text = Convert.ToDateTime(dt.Rows[0]["InspectDate"]).ToString("yyyy-MM-dd");
                txt4.Text = dt.Rows[0]["looseequipmenttype"] == DBNull.Value ? "-" : dt.Rows[0]["looseequipmenttype"].ToString();

              
                txt5.Text = dt.Rows[0]["Number"] == DBNull.Value ? "-" : dt.Rows[0]["Number"].ToString();
                txt6.Text = dt.Rows[0]["Condition"] == DBNull.Value ? "-" : dt.Rows[0]["Condition"].ToString();
                txt7.Text = dt.Rows[0]["Remarks"] == DBNull.Value ? "-" : dt.Rows[0]["Remarks"].ToString();
              
              
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }
    }
}
