using DataBuildingLayer;
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
    /// Interaction logic for ViewLooseEDamage.xaml
    /// </summary>
    public partial class ViewLooseEDamage : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewLooseEDamage()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
            int looseEtypeid = 0;
            int id = StaticHelper.ViewId;
          
            SqlDataAdapter adp = new SqlDataAdapter("select a.id,a.looseetypeid, a.certificateNumber,a.DamageDate,a.IncidentReport,a.DamageReason,a.damageObserved,a.MOpId,b.looseequipmenttype from LooseEDamageRecord a inner join LooseEType b on a.LooseETypeId=b.Id where a.id=" + id + "", sc.con);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                looseEtypeid= Convert.ToInt32(dt.Rows[0]["looseetypeid"]);
                txt1.Text = dt.Rows[0]["looseequipmenttype"].ToString();

                txt2.Text = dt.Rows[0]["certificateNumber"] == DBNull.Value ? "-" : dt.Rows[0]["certificateNumber"].ToString();
                txt5.Text = Convert.ToDateTime(dt.Rows[0]["DamageDate"]).ToString("yyyy-MM-dd");
                txt3.Text = dt.Rows[0]["damageObserved"] == DBNull.Value ? "-" : dt.Rows[0]["damageObserved"].ToString();

                txt4.Text = dt.Rows[0]["DamageReason"] == DBNull.Value ? "-" : dt.Rows[0]["DamageReason"].ToString();
                //txt5.Text = dt.Rows[0]["DamageDate"] == DBNull.Value ? "-" : dt.Rows[0]["DamageDate"].ToString();
                txt6.Text = dt.Rows[0]["IncidentReport"] == DBNull.Value ? "-" : dt.Rows[0]["IncidentReport"].ToString();
                var opid = dt.Rows[0]["MOpId"].ToString();
                if(string.IsNullOrEmpty(opid) == false && txt3.Text == "Mooring Operation")
                {
                    using (SqlDataAdapter adpo = new SqlDataAdapter("select OPId, (portname + ' - '+ convert(varchar, fastdatetime, 106)) as Operation from  MOperationBirthDetail where IsActive=1 and OPId = "+ opid + "", sc.con))
                    {
                        DataTable dto = new DataTable();
                        adpo.Fill(dto);

                        txt7.Text = dto.Rows[0]["Operation"].ToString() ;
                    }
                }

            }


            //SqlDataAdapter adp11 = new SqlDataAdapter("select AssignWinchId from AssignLooseEquipToWinch where LooseETypeId='" + looseEtypeid + "'", sc.con);
            //DataTable dt11 = new DataTable();
            //adp11.Fill(dt11);
            //if (dt11.Rows.Count > 0)
            //{
            //    int asswinchid = Convert.ToInt32(dt11.Rows[0][0]);
            //    txt4.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.AssignedNumber).FirstOrDefault();
            //    txt5.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.Location).FirstOrDefault();
            //}
            //else
            //{
            //    txt4.Text = "Not Assigned";
            //    txt5.Text = "Not Assigned";
            //}
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
