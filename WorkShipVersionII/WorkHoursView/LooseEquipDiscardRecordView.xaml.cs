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
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for LooseEquipDiscardRecordView.xaml
    /// </summary>
    public partial class LooseEquipDiscardRecordView : UserControl
    {
        readonly static LooseEquipDiscardRecordViewModel _ropedamageClass = new LooseEquipDiscardRecordViewModel();
        private readonly ShipmentContaxt sc;
        public LooseEquipDiscardRecordView()
        {
            InitializeComponent();
            sc = new ShipmentContaxt();

            cboBhp.SelectedIndex = 0;
            cboBhp2.SelectedIndex = 0;
            cmbCertino.SelectedIndex = 0;
            comboReOutofSer.SelectedIndex = 0;



           // _ropedamageClass.Refreshmethod();

        }

        //private void comboRope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddLooseEDamageRecordViewModel.sropetype.Id).FirstOrDefault();
        //    //txtOutboard.Text = data.Outboard == true ? "A" : "B";
        //    txtAssignedLocation.Text = data.AssignedLocation;
        //    //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
        //    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId).Select(x => x.AssignedNumber).FirstOrDefault();


        //}



        private void cboBhp_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = cboBhp.Text.ToString();

            if (damageOb == "Mooring Operation")
            {
                bntSection.SetValue(Grid.RowProperty, 15);
                lblmoorop.Visibility = Visibility.Visible;
                cboBhp2.Visibility = Visibility.Visible;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 15);
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;

            }
        }

        private void comboRope_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string damageOb = comboRope.Text.ToString();

                if (damageOb != "--Select--")
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Int32 id = Convert.ToInt32(dt.Rows[0][0]);

                    if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                    {
                        // SqlDataAdapter adp1 = new SqlDataAdapter("select CertificateNumber from RopeTail where LooseETypeId='" + id + "' and OutofServiceDate is null and DeleteStatus=0", sc.con);
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from RopeTail where LooseETypeId='" + id + "' and OutofServiceDate is null and DeleteStatus=0", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            // cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            cmbCertino.SelectedIndex = 0;

                            btnSave.IsEnabled = true;

                            LooseEquipDiscardRecordViewModel._ropedamageClass.Id = Convert.ToInt32(cmbCertino.SelectedValue);
                            LooseEquipDiscardRecordViewModel._ropedamageClass.CertificateNumber = cmbCertino.Text.ToString();
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }

                        //SqlDataAdapter adp11 = new SqlDataAdapter("select AssignWinchId from AssignLooseEquipToWinch where LooseETypeId='" + id + "'", sc.con);
                        //DataTable dt11 = new DataTable();
                        //adp11.Fill(dt11);
                        //if (dt11.Rows.Count > 0)
                        //{
                        //    int asswinchid = Convert.ToInt32(dt11.Rows[0][0]);
                        //    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.AssignedNumber).FirstOrDefault();
                        //    txtAssignedLocation.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.Location).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    txtAssignedWinch.Text = "Not Assigned";
                        //    txtAssignedLocation.Text = "Not Assigned";
                        //}
                    }
                    if (damageOb == "Joining Shackle")
                    {
                        DataRow dr;
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from JoiningShackle where LooseETypeId='" + id + "' and OutofServiceDate is null and DeleteStatus=0", sc.con);
                       // SqlDataAdapter adp1 = new SqlDataAdapter("select CertificateNumber from JoiningShackle where LooseETypeId='" + id + "' and OutofServiceDate is null and DeleteStatus=0", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();

                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;

                            LooseEquipDiscardRecordViewModel._ropedamageClass.Id = Convert.ToInt32(cmbCertino.SelectedValue);
                            LooseEquipDiscardRecordViewModel._ropedamageClass.CertificateNumber = cmbCertino.Text.ToString();
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }
                        //SqlDataAdapter adp11 = new SqlDataAdapter("select AssignWinchId from AssignLooseEquipToWinch where LooseETypeId='" + id + "'", sc.con);
                        //DataTable dt11 = new DataTable();
                        //adp11.Fill(dt11);
                        //if (dt11.Rows.Count > 0)
                        //{
                        //    int asswinchid = Convert.ToInt32(dt11.Rows[0][0]);
                        //    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.AssignedNumber).FirstOrDefault();
                        //    txtAssignedLocation.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.Location).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    txtAssignedWinch.Text = "Not Assigned";
                        //    txtAssignedLocation.Text = "Not Assigned";
                        //}

                    }
                    if (damageOb == "Chain Stopper")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from ChainStopper where LooseETypeId='" + id + "' and OutofServiceDate is null and DeleteStatus=0", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;

                            LooseEquipDiscardRecordViewModel._ropedamageClass.Id = Convert.ToInt32(cmbCertino.SelectedValue);
                            LooseEquipDiscardRecordViewModel._ropedamageClass.CertificateNumber = cmbCertino.Text.ToString();
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }

                        //SqlDataAdapter adp11 = new SqlDataAdapter("select AssignWinchId from AssignLooseEquipToWinch where LooseETypeId='" + id + "'", sc.con);
                        //DataTable dt11 = new DataTable();
                        //adp11.Fill(dt11);
                        //if (dt11.Rows.Count > 0)
                        //{
                        //    int asswinchid = Convert.ToInt32(dt11.Rows[0][0]);
                        //    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.AssignedNumber).FirstOrDefault();
                        //    txtAssignedLocation.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.Location).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    txtAssignedWinch.Text = "Not Assigned";
                        //    txtAssignedLocation.Text = "Not Assigned";
                        //}
                    }
                    if (damageOb == "Chafe Guard")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from ChafeGuard where OutofServiceDate is null and DeleteStatus=0", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;

                            LooseEquipDiscardRecordViewModel._ropedamageClass.Id = Convert.ToInt32(cmbCertino.SelectedValue);
                            LooseEquipDiscardRecordViewModel._ropedamageClass.CertificateNumber = cmbCertino.Text.ToString();
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }

                        //SqlDataAdapter adp11 = new SqlDataAdapter("select AssignWinchId from AssignLooseEquipToWinch where LooseETypeId='" + id + "'", sc.con);
                        //DataTable dt11 = new DataTable();
                        //adp11.Fill(dt11);
                        //if (dt11.Rows.Count > 0)
                        //{
                        //    int asswinchid = Convert.ToInt32(dt11.Rows[0][0]);
                        //    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.AssignedNumber).FirstOrDefault();
                        //    txtAssignedLocation.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.Location).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    txtAssignedWinch.Text = "Not Assigned";
                        //    txtAssignedLocation.Text = "Not Assigned";
                        //}
                    }
                    if (damageOb == "Winch Brake Test Kit")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from WinchBreakTestKit where OutofServiceDate is null and DeleteStatus=0", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;

                            LooseEquipDiscardRecordViewModel._ropedamageClass.Id = Convert.ToInt32(cmbCertino.SelectedValue);
                            LooseEquipDiscardRecordViewModel._ropedamageClass.CertificateNumber = cmbCertino.Text.ToString();
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }

                        //SqlDataAdapter adp11 = new SqlDataAdapter("select AssignWinchId from AssignLooseEquipToWinch where LooseETypeId='" + id + "'", sc.con);
                        //DataTable dt11 = new DataTable();
                        //adp11.Fill(dt11);
                        //if (dt11.Rows.Count > 0)
                        //{
                        //    int asswinchid = Convert.ToInt32(dt11.Rows[0][0]);
                        //    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.AssignedNumber).FirstOrDefault();
                        //    txtAssignedLocation.Text = sc.MooringWinch.Where(s => s.Id == asswinchid).Select(x => x.Location).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    txtAssignedWinch.Text = "Not Assigned";
                        //    txtAssignedLocation.Text = "Not Assigned";
                        //}
                    }
                }
            }
            catch { }

        }

        private void cmbCertino_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = cmbCertino.Text.ToString();
            if (damageOb != "" && damageOb != "--Select--")
            {
                int idd = Convert.ToInt32(cmbCertino.SelectedValue);

                txtCertino.Text = damageOb;
                btnSave.IsEnabled = true;

                LooseEquipDiscardRecordViewModel._ropedamageClass.Id = idd;
                LooseEquipDiscardRecordViewModel._ropedamageClass.CertificateNumber = damageOb;
            }
            else
            {
                
                txtCertino.Text = "";
                btnSave.IsEnabled = false;
            }
        }

        private void comboReOutofSer_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboReOutofSer.Text.ToString();

            if (damageOb == "Other")
            {
                bntSection.SetValue(Grid.RowProperty, 15);
                lblothrs.Visibility = Visibility.Visible;
                txtotherreason.Visibility = Visibility.Visible;
                lbldmg.Visibility = Visibility.Hidden;
                cboBhp.Visibility = Visibility.Hidden;
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;

                cboBhp2.SelectedIndex = 0;
                cboBhp.SelectedIndex = 0;
            }
            else if(damageOb == "Damaged")
            {
                bntSection.SetValue(Grid.RowProperty, 15);
                bntSection.SetValue(Grid.RowProperty, 15);
                lbldmg.Visibility = Visibility.Visible;
                cboBhp.Visibility= Visibility.Visible;
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;

            }
            else
            {
                cboBhp2.SelectedIndex = 0;
                cboBhp.SelectedIndex = 0;
                bntSection.SetValue(Grid.RowProperty, 15);
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;
                lbldmg.Visibility = Visibility.Hidden;
                cboBhp.Visibility = Visibility.Hidden;
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;

            }
        }

        private void DpOutofSer_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpOutofSer.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = GetRopeReceivedDate();
                var dateto = Convert.ToDateTime(to);

                if (dateto > datefrom)
                {
                    MessageBox.Show("Discard Date can not be less than Loose Eq. Received Date!", "Loose Eq.", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpOutofSer.Text = to;
                }
            }
            catch { }
        }

        private string GetRopeReceivedDate()
        {
            string dtt = "";
            try
            {
                string damageOb = comboRope.Text.ToString();
                if (damageOb != "--Select--")
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Int32 id = Convert.ToInt32(dt.Rows[0][0]);

                    if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select ReceivedDate from RopeTail where LooseETypeId='" + id + "' and OutofServiceDate is null", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }
                    if (damageOb == "Joining Shackle")
                    {
                        DataRow dr;
                        SqlDataAdapter adp1 = new SqlDataAdapter("select DateReceived from JoiningShackle where LooseETypeId='" + id + "' and OutofServiceDate is null", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }
                    if (damageOb == "Chain Stopper")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select DateReceived from ChainStopper where LooseETypeId='" + id + "' and OutofServiceDate is null", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }

                    if (damageOb == "Chafe Guard")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select ReceivedDate from chafeguard where CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }
                    if (damageOb == "Winch Brake Test Kit")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select ReceivedDate from WinchBreakTestKit where CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }
                }
                return dtt;


            }
            catch { return dtt; }
        }
    }
}
