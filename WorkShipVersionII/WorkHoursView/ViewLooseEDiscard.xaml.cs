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
    /// Interaction logic for ViewLooseEDiscard.xaml
    /// </summary>
    public partial class ViewLooseEDiscard : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewLooseEDiscard()
        {
            InitializeComponent();

            try
            {
                if (sc == null)
                {
                    sc = new ShipmentContaxt();
                }

                int id = StaticHelper.ViewId;
                string certificateno = StaticHelper.SearchText;

                
                if(id==1)
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select a.looseetypeid,a.CertificateNumber,a.UniqueId, a.outofservicedate, case when a.ReasonOutofService='Other' then a.OtherReason else a.ReasonOutofService  end as reasonoutofservice, b.looseequipmenttype from JoiningShackle a inner join LooseEType b on a.LooseETypeId=b.Id where a.certificatenumber='" + certificateno + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txt2.Text = dt.Rows[0]["certificatenumber"].ToString();
                        txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();
                        txt1.Text = dt.Rows[0]["looseequipmenttype"] == DBNull.Value ? "-" : dt.Rows[0]["looseequipmenttype"].ToString();
                        txt3.Text = Convert.ToDateTime(dt.Rows[0]["outofservicedate"]).ToString("yyyy-MM-dd");
                        txt4.Text = dt.Rows[0]["reasonoutofservice"] == DBNull.Value ? "-" : dt.Rows[0]["reasonoutofservice"].ToString();
                    }
                }
               else if(id==5)
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select a.looseetypeid ,a.CertificateNumber,a.UniqueId, a.outofservicedate, case when a.ReasonOutofService='Other' then a.OtherReason else a.ReasonOutofService  end as reasonoutofservice, b.looseequipmenttype from chainstopper a inner join LooseEType b on a.LooseETypeId=b.Id where a.certificatenumber='" + certificateno + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        txt2.Text = dt.Rows[0]["certificatenumber"].ToString();
                        txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();
                        txt1.Text = dt.Rows[0]["looseequipmenttype"] == DBNull.Value ? "-" : dt.Rows[0]["looseequipmenttype"].ToString();
                        txt3.Text = Convert.ToDateTime(dt.Rows[0]["outofservicedate"]).ToString("yyyy-MM-dd");
                        txt4.Text = dt.Rows[0]["reasonoutofservice"] == DBNull.Value ? "-" : dt.Rows[0]["reasonoutofservice"].ToString();
                    }
                }
                else if (id == 7)
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select a.CertificateNumber,a.UniqueId, a.outofservicedate, case when a.ReasonOutofService='Other' then a.OtherReason else a.ReasonOutofService   end as reasonoutofservice,'Chafe Guard' as looseequipmenttype from chafeguard a where a.certificatenumber='" + certificateno + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        txt2.Text = dt.Rows[0]["certificatenumber"].ToString();
                        txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();
                        txt1.Text = dt.Rows[0]["looseequipmenttype"] == DBNull.Value ? "-" : dt.Rows[0]["looseequipmenttype"].ToString();
                        txt3.Text = Convert.ToDateTime(dt.Rows[0]["outofservicedate"]).ToString("yyyy-MM-dd");
                        txt4.Text = dt.Rows[0]["reasonoutofservice"] == DBNull.Value ? "-" : dt.Rows[0]["reasonoutofservice"].ToString();
                    }
                }
                else if (id == 8)
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select a.CertificateNumber,a.UniqueId, a.outofservicedate, case when a.ReasonOutofService='Other' then a.OtherReason else a.ReasonOutofService   end as reasonoutofservice,'Winch Brake Test Kit' as looseequipmenttype from WinchBreakTestKit a where a.certificatenumber='" + certificateno + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        txt2.Text = dt.Rows[0]["certificatenumber"].ToString();
                        txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();
                        txt1.Text = dt.Rows[0]["looseequipmenttype"] == DBNull.Value ? "-" : dt.Rows[0]["looseequipmenttype"].ToString();
                        txt3.Text = Convert.ToDateTime(dt.Rows[0]["outofservicedate"]).ToString("yyyy-MM-dd");
                        txt4.Text = dt.Rows[0]["reasonoutofservice"] == DBNull.Value ? "-" : dt.Rows[0]["reasonoutofservice"].ToString();
                    }
                }
                else
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select a.looseetypeid ,a.CertificateNumber,a.UniqueId, a.outofservicedate, case when a.ReasonOutofService='Other' then a.OtherReason else a.ReasonOutofService  end as reasonoutofservice, b.looseequipmenttype from ropetail a inner join LooseEType b on a.LooseETypeId=b.Id where a.certificatenumber='" + certificateno + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        txt2.Text = dt.Rows[0]["certificatenumber"].ToString();
                        txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();
                        txt1.Text = dt.Rows[0]["looseequipmenttype"] == DBNull.Value ? "-" : dt.Rows[0]["looseequipmenttype"].ToString();
                        txt3.Text = Convert.ToDateTime(dt.Rows[0]["outofservicedate"]).ToString("yyyy-MM-dd");
                        txt4.Text = dt.Rows[0]["reasonoutofservice"] == DBNull.Value ? "-" : dt.Rows[0]["reasonoutofservice"].ToString();
                    }

                }
               
             
            }
            catch { }
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
