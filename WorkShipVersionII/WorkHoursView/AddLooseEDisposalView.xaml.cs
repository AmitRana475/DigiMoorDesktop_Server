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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for AddLooseEDisposalView.xaml
    /// </summary>
    public partial class AddLooseEDisposalView : UserControl
    {
        private readonly ShipmentContaxt sc;

        static string discarddate = "";
        public AddLooseEDisposalView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();
            dpRecDate.DisplayDateEnd = DateTime.Now.Date;
        }




        //private void bindLooseEType()
        //{
        //    SqlDataAdapter adp1 = new SqlDataAdapter("select * from looseetype", sc.con);
        //    DataSet dt1 = new DataSet();
        //    adp1.Fill(dt1);
        //    if (dt1.Tables[0].Rows.Count > 0)
        //    {

        //        comboLooseEtype.ItemsSource = dt1.Tables[0].DefaultView;
        //        comboLooseEtype.DisplayMemberPath = dt1.Tables[0].Columns["LooseEquipmentType"].ToString();
        //        comboLooseEtype.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();

        //    }
        //}

        private void comboLooseEtype_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string damageOb = comboLooseEtype.Text.ToString();
                if (damageOb != "--Select--")
                {
                    btnSave.IsEnabled = true;
                    SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Int32 id = Convert.ToInt32(dt.Rows[0][0]);


                    AddLooseEDisposalViewModel._AddLooseEDisposal.LooseETypeId = id;
                    if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                    {
                      

                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from RopeTail where LooseETypeId='" + id + "'  and CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            // cmbCertino.SelectedIndex = 0;

                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;
                            AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text.ToString();

                            var outofserdt = sc.RopeTails.Where(x => x.CertificateNumber == cmbCertino.Text && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();

                            if (outofserdt == null)
                            {
                                btnSave.IsEnabled = false;
                                MessageBox.Show("Please mark this Loose equipment as 'discarded' from 'Loose Equipment Discard form' before disposing", "Loose Eq. Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            }
                        else
                        {
                           
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }
                    }
                    if (damageOb == "Joining Shackle")
                    {
                        DataRow dr;
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from JoiningShackle where LooseETypeId='" + id + "' and CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            //cmbCertino.SelectedIndex = 0;

                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;
                            AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text.ToString();

                            var outofserdt = sc.JoiningShackles.Where(x => x.CertificateNumber == cmbCertino.Text && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();

                            if (outofserdt == null)
                            {
                                btnSave.IsEnabled = false;
                                MessageBox.Show("Please mark this Loose equipment as 'discarded' from 'Loose Equipment Discard form' before disposing", "Loose Eq. Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                           // MessageBox.Show("Please mark this Loose equipment as 'disposed' from 'Loose Equipment Discard form' before disposing", "Loose Equip Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }
                    }
                    if (damageOb == "Chain Stopper")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from ChainStopper where LooseETypeId='" + id + "' and CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();

                            //cmbCertino.SelectedIndex = 0;

                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;
                            AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text.ToString();

                            var outofserdt = sc.ChainStoppers.Where(x => x.CertificateNumber == cmbCertino.Text && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();

                            if (outofserdt == null)
                            {
                                btnSave.IsEnabled = false;
                                MessageBox.Show("Please mark this Loose equipment as 'discarded' from 'Loose Equipment Discard form' before disposing", "Loose Eq. Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Please mark this Loose equipment as 'disposed' from 'Loose Equipment Discard form' before disposing", "Loose Equip Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }
                    }
                    if (damageOb == "Chafe Guard")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from chafeguard where CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            // cmbCertino.SelectedIndex = 0;

                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;
                            AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text.ToString();

                            var outofserdt = sc.ChafeGuard.Where(x => x.CertificateNumber == cmbCertino.Text && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();

                            if (outofserdt == null)
                            {
                                btnSave.IsEnabled = false;
                                MessageBox.Show("Please mark this Loose equipment as 'discarded' from 'Loose Equipment Discard form' before disposing", "Loose Eq. Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Please mark this Loose equipment as 'disposed' from 'Loose Equipment Discard form' before disposing", "Loose Equip Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }
                    }
                    if (damageOb == "Winch Brake Test Kit")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select Id, CertificateNumber + ' - ' + UniqueID as CertificateNumber from WinchBreakTestKit where CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {

                            cmbCertino.ItemsSource = dt1.Tables[0].DefaultView;
                            cmbCertino.DisplayMemberPath = dt1.Tables[0].Columns["certificatenumber"].ToString();
                            cmbCertino.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();
                            //  cmbCertino.SelectedIndex = 0;
                            cmbCertino.SelectedIndex = 0;
                            btnSave.IsEnabled = true;
                            AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text.ToString();

                            var outofserdt = sc.WBTestKit.Where(x => x.CertificateNumber == cmbCertino.Text && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();

                            if (outofserdt == null)
                            {
                                btnSave.IsEnabled = false;
                                MessageBox.Show("Please mark this Loose equipment as 'discarded' from 'Loose Equipment Discard form' before disposing", "Loose Eq. Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            }

                        }
                        else
                        {
                           // MessageBox.Show("Please mark this Loose equipment as 'disposed' from 'Loose Equipment Discard form' before disposing", "Loose Equip Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                            btnSave.IsEnabled = false;
                            cmbCertino.ItemsSource = null;
                        }
                    }
                }
            }
            catch { }
        }

        private void cmbCertino_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string damageOb = comboLooseEtype.Text.ToString();
                if (damageOb != "--Select--")
                {
                    int idd = Convert.ToInt32(cmbCertino.SelectedValue);

                    SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Int32 id = Convert.ToInt32(dt.Rows[0][0]);

                    if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                    {

                        AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text;

                        //SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from RopeTail where CertificateNumber='" + cmbCertino.Text + "'", sc.con);
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from RopeTail where Id='" + idd + "'", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            var outofserdt = dt1.Tables[0].Rows[0][0].ToString();

                           

                            if (outofserdt == null || outofserdt == "")
                            {
                                txtmessage.Visibility = Visibility.Visible;


                                lbl1.Visibility = Visibility.Hidden;
                                lbl2.Visibility = Visibility.Hidden;
                                lbl3.Visibility = Visibility.Hidden;

                                lblDisDt.Visibility = Visibility.Hidden;
                                lbldtoutofs.Visibility = Visibility.Hidden;
                                lblprtname.Visibility = Visibility.Hidden;
                                lblRecep.Visibility = Visibility.Hidden;

                                rpdis.Height = 250;

                                txtDisPrtN.Visibility = Visibility.Hidden;
                                txtdtOutofS.Visibility = Visibility.Hidden;
                                txtReceFaciN.Visibility = Visibility.Hidden;
                                dpRecDate.Visibility = Visibility.Hidden;

                                btnSave.Visibility = Visibility.Hidden;
                                btnCancel.Visibility = Visibility.Hidden;

                                txtmessage.Text = "Equipment not yet Out of Service, Please fill Rope Discard Form before filling the Disposal Form.";


                            }
                            else
                            {
                                rpdis.Height = 490;
                                txtmessage.Visibility = Visibility.Hidden;




                                lbl1.Visibility = Visibility.Visible;
                                lbl2.Visibility = Visibility.Visible;
                                lbl3.Visibility = Visibility.Visible;
                                lblDisDt.Visibility = Visibility.Visible;
                                lbldtoutofs.Visibility = Visibility.Visible;
                                lblprtname.Visibility = Visibility.Visible;
                                lblRecep.Visibility = Visibility.Visible;

                                btnSave.IsEnabled = true;
                                txtDisPrtN.Visibility = Visibility.Visible;
                                txtdtOutofS.Visibility = Visibility.Visible;
                                txtReceFaciN.Visibility = Visibility.Visible;
                                dpRecDate.Visibility = Visibility.Visible;

                                lblDisDt.Visibility = Visibility.Visible;

                                btnSave.Visibility = Visibility.Visible;
                                btnCancel.Visibility = Visibility.Visible;


                                AddLooseEDisposalViewModel._AddLooseEDisposal.DiscardedDate = Convert.ToDateTime(outofserdt);

                                DateTime dttmie = Convert.ToDateTime(outofserdt);

                                discarddate = outofserdt;

                                string ss = dttmie.ToString("dddd, dd MMMM yyyy");
                                txtdtOutofS.Text = ss;

                            }
                        }
                        else
                        {
                            txtdtOutofS.Text = "";
                        }
                    }
                    if (damageOb == "Joining Shackle")
                    {
                        AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text;

                        DataRow dr;
                        //SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from JoiningShackle where CertificateNumber='" + cmbCertino.Text + "'", sc.con);
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from JoiningShackle where Id='" + idd + "'", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            var outofserdt = dt1.Tables[0].Rows[0][0].ToString();

                         
                            if (outofserdt == null || outofserdt == "")
                            {
                                txtmessage.Visibility = Visibility.Visible;



                                lbl1.Visibility = Visibility.Hidden;
                                lbl2.Visibility = Visibility.Hidden;
                                lbl3.Visibility = Visibility.Hidden;
                                lblDisDt.Visibility = Visibility.Hidden;
                                lbldtoutofs.Visibility = Visibility.Hidden;
                                lblprtname.Visibility = Visibility.Hidden;
                                lblRecep.Visibility = Visibility.Hidden;

                                rpdis.Height = 250;

                                txtDisPrtN.Visibility = Visibility.Hidden;
                                txtdtOutofS.Visibility = Visibility.Hidden;
                                txtReceFaciN.Visibility = Visibility.Hidden;
                                dpRecDate.Visibility = Visibility.Hidden;

                                btnSave.Visibility = Visibility.Hidden;
                                btnCancel.Visibility = Visibility.Hidden;

                                txtmessage.Text = "Equipment not yet Out of Service, Please fill Rope Discard Form before filling the Disposal Form.";


                            }
                            else
                            {
                                rpdis.Height = 490;
                                txtmessage.Visibility = Visibility.Hidden;



                                lbl1.Visibility = Visibility.Visible;
                                lbl2.Visibility = Visibility.Visible;
                                lbl3.Visibility = Visibility.Visible;
                                lblDisDt.Visibility = Visibility.Visible;
                                lbldtoutofs.Visibility = Visibility.Visible;
                                lblprtname.Visibility = Visibility.Visible;
                                lblRecep.Visibility = Visibility.Visible;

                                btnSave.IsEnabled = true;

                                txtDisPrtN.Visibility = Visibility.Visible;
                                txtdtOutofS.Visibility = Visibility.Visible;
                                txtReceFaciN.Visibility = Visibility.Visible;
                                dpRecDate.Visibility = Visibility.Visible;

                                lblDisDt.Visibility = Visibility.Visible;

                                btnSave.Visibility = Visibility.Visible;
                                btnCancel.Visibility = Visibility.Visible;


                                AddLooseEDisposalViewModel._AddLooseEDisposal.DiscardedDate = Convert.ToDateTime(outofserdt);

                                DateTime dttmie = Convert.ToDateTime(outofserdt);

                                discarddate = outofserdt;
                                string ss = dttmie.ToString("dddd, dd MMMM yyyy");
                                txtdtOutofS.Text = ss;

                            }
                        }
                        else
                        {
                            txtdtOutofS.Text = "";
                        }
                    }
                    if (damageOb == "Chain Stopper")
                    {
                        AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text;

                        //SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from ChainStopper where CertificateNumber='" + cmbCertino.Text + "'", sc.con);
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from ChainStopper where Id='" + idd + "'", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            var outofserdt = dt1.Tables[0].Rows[0][0].ToString();

         

                            if (outofserdt == null || outofserdt == "")
                            {
                                txtmessage.Visibility = Visibility.Visible;




                                lbl1.Visibility = Visibility.Hidden;
                                lbl2.Visibility = Visibility.Hidden;
                                lbl3.Visibility = Visibility.Hidden;
                                lblDisDt.Visibility = Visibility.Hidden;
                                lbldtoutofs.Visibility = Visibility.Hidden;
                                lblprtname.Visibility = Visibility.Hidden;
                                lblRecep.Visibility = Visibility.Hidden;

                                rpdis.Height = 250;

                                txtDisPrtN.Visibility = Visibility.Hidden;
                                txtdtOutofS.Visibility = Visibility.Hidden;
                                txtReceFaciN.Visibility = Visibility.Hidden;
                                dpRecDate.Visibility = Visibility.Hidden;

                                btnSave.Visibility = Visibility.Hidden;
                                btnCancel.Visibility = Visibility.Hidden;

                                txtmessage.Text = "Equipment not yet Out of Service, Please fill Rope Discard Form before filling the Disposal Form.";


                            }
                            else
                            {
                                rpdis.Height = 490;
                                txtmessage.Visibility = Visibility.Hidden;




                                lbl1.Visibility = Visibility.Visible;
                                lbl2.Visibility = Visibility.Visible;
                                lbl3.Visibility = Visibility.Visible;
                                lblDisDt.Visibility = Visibility.Visible;
                                lbldtoutofs.Visibility = Visibility.Visible;
                                lblprtname.Visibility = Visibility.Visible;
                                lblRecep.Visibility = Visibility.Visible;

                                btnSave.IsEnabled = true;
                                txtDisPrtN.Visibility = Visibility.Visible;
                                txtdtOutofS.Visibility = Visibility.Visible;
                                txtReceFaciN.Visibility = Visibility.Visible;
                                dpRecDate.Visibility = Visibility.Visible;

                                lblDisDt.Visibility = Visibility.Visible;

                                btnSave.Visibility = Visibility.Visible;
                                btnCancel.Visibility = Visibility.Visible;


                                AddLooseEDisposalViewModel._AddLooseEDisposal.DiscardedDate = Convert.ToDateTime(outofserdt);
                                DateTime dttmie = Convert.ToDateTime(outofserdt);

                                discarddate = outofserdt;
                                string ss = dttmie.ToString("dddd, dd MMMM yyyy");
                                txtdtOutofS.Text = ss;

                            }
                        }
                        else
                        {
                            txtdtOutofS.Text = "";
                        }
                    }

                    if (damageOb == "Chafe Guard")
                    {
                        AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text;

                        //SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from ChafeGuard where CertificateNumber='" + cmbCertino.Text + "'", sc.con);
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from ChafeGuard where Id='" + idd + "'", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            var outofserdt = dt1.Tables[0].Rows[0][0].ToString();




                            if (outofserdt == null || outofserdt == "")
                            {
                                txtmessage.Visibility = Visibility.Visible;




                                lbl1.Visibility = Visibility.Hidden;
                                lbl2.Visibility = Visibility.Hidden;
                                lbl3.Visibility = Visibility.Hidden;
                                lblDisDt.Visibility = Visibility.Hidden;
                                lbldtoutofs.Visibility = Visibility.Hidden;
                                lblprtname.Visibility = Visibility.Hidden;
                                lblRecep.Visibility = Visibility.Hidden;

                                rpdis.Height = 250;

                                txtDisPrtN.Visibility = Visibility.Hidden;
                                txtdtOutofS.Visibility = Visibility.Hidden;
                                txtReceFaciN.Visibility = Visibility.Hidden;
                                dpRecDate.Visibility = Visibility.Hidden;

                                btnSave.Visibility = Visibility.Hidden;
                                btnCancel.Visibility = Visibility.Hidden;

                                txtmessage.Text = "Equipment not yet Out of Service, Please fill Rope Discard Form before filling the Disposal Form.";


                            }
                            else
                            {
                                rpdis.Height = 490;
                                txtmessage.Visibility = Visibility.Hidden;




                                lbl1.Visibility = Visibility.Visible;
                                lbl2.Visibility = Visibility.Visible;
                                lbl3.Visibility = Visibility.Visible;
                                lblDisDt.Visibility = Visibility.Visible;
                                lbldtoutofs.Visibility = Visibility.Visible;
                                lblprtname.Visibility = Visibility.Visible;
                                lblRecep.Visibility = Visibility.Visible;

                                btnSave.IsEnabled = true;
                                txtDisPrtN.Visibility = Visibility.Visible;
                                txtdtOutofS.Visibility = Visibility.Visible;
                                txtReceFaciN.Visibility = Visibility.Visible;
                                dpRecDate.Visibility = Visibility.Visible;

                                lblDisDt.Visibility = Visibility.Visible;

                                btnSave.Visibility = Visibility.Visible;
                                btnCancel.Visibility = Visibility.Visible;


                                AddLooseEDisposalViewModel._AddLooseEDisposal.DiscardedDate = Convert.ToDateTime(outofserdt);
                                DateTime dttmie = Convert.ToDateTime(outofserdt);

                                discarddate = outofserdt;
                                string ss = dttmie.ToString("dddd, dd MMMM yyyy");
                                txtdtOutofS.Text = ss;

                            }
                        }
                        else
                        {
                            txtdtOutofS.Text = "";
                        }
                    }

                    if (damageOb == "Winch Brake Test Kit")
                    {
                        AddLooseEDisposalViewModel._AddLooseEDisposal.LooseECertiNo = cmbCertino.Text;

                        //SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from WinchBreakTestKit where CertificateNumber='" + cmbCertino.Text + "'", sc.con);
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutOfServiceDate from WinchBreakTestKit where Id ='" + idd + "'", sc.con);
                        DataSet dt1 = new DataSet();
                        adp1.Fill(dt1);
                        if (dt1.Tables[0].Rows.Count > 0)
                        {
                            var outofserdt = dt1.Tables[0].Rows[0][0].ToString();



                            if (outofserdt == null || outofserdt == "")
                            {
                                txtmessage.Visibility = Visibility.Visible;



                                lbl1.Visibility = Visibility.Hidden;
                                lbl2.Visibility = Visibility.Hidden;
                                lbl3.Visibility = Visibility.Hidden;
                                lblDisDt.Visibility = Visibility.Hidden;
                                lbldtoutofs.Visibility = Visibility.Hidden;
                                lblprtname.Visibility = Visibility.Hidden;
                                lblRecep.Visibility = Visibility.Hidden;

                                rpdis.Height = 250;

                                txtDisPrtN.Visibility = Visibility.Hidden;
                                txtdtOutofS.Visibility = Visibility.Hidden;
                                txtReceFaciN.Visibility = Visibility.Hidden;
                                dpRecDate.Visibility = Visibility.Hidden;

                                btnSave.Visibility = Visibility.Hidden;
                                btnCancel.Visibility = Visibility.Hidden;

                                txtmessage.Text = "Equipment not yet Out of Service, Please fill Rope Discard Form before filling the Disposal Form.";


                            }
                            else
                            {
                                rpdis.Height = 490;
                                txtmessage.Visibility = Visibility.Hidden;




                                lbl1.Visibility = Visibility.Visible;
                                lbl2.Visibility = Visibility.Visible;
                                lbl3.Visibility = Visibility.Visible;
                                lblDisDt.Visibility = Visibility.Visible;
                                lbldtoutofs.Visibility = Visibility.Visible;
                                lblprtname.Visibility = Visibility.Visible;
                                lblRecep.Visibility = Visibility.Visible;


                                btnSave.IsEnabled = true;

                                txtDisPrtN.Visibility = Visibility.Visible;
                                txtdtOutofS.Visibility = Visibility.Visible;
                                txtReceFaciN.Visibility = Visibility.Visible;
                                dpRecDate.Visibility = Visibility.Visible;

                                lblDisDt.Visibility = Visibility.Visible;

                                btnSave.Visibility = Visibility.Visible;
                                btnCancel.Visibility = Visibility.Visible;

                                AddLooseEDisposalViewModel._AddLooseEDisposal.DiscardedDate = Convert.ToDateTime(outofserdt);

                                DateTime dttmie = Convert.ToDateTime(outofserdt);

                                discarddate = outofserdt;
                                string ss = dttmie.ToString("dddd, dd MMMM yyyy");
                                txtdtOutofS.Text = ss;

                            }
                        }
                        else
                        {
                            txtdtOutofS.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void DpRecDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                //var to = GetRopeReceivedDate();
                var to = discarddate;
                var dateto = Convert.ToDateTime(to);

                if (dateto > datefrom)
                {
                    MessageBox.Show("Disposal Date can not be less than Loose Eq. Discard Date!", "Loose Eq.", MessageBoxButton.OK, MessageBoxImage.Error);
                    //dpRecDate.Text = to;
                    dpRecDate.Text = DateTime.Now.ToString();
                }
            }
            catch { }
        }

        private string GetRopeReceivedDate()
        {
            string dtt = "";
            try
            {
                string damageOb = comboLooseEtype.Text.ToString();
                if (damageOb != "--Select--")
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    Int32 id = Convert.ToInt32(dt.Rows[0][0]);

                    if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutofServiceDate from RopeTail where LooseETypeId='" + id + "'", sc.con);
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
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutofServiceDate from JoiningShackle where LooseETypeId='" + id + "'", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }
                    if (damageOb == "Chain Stopper")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutofServiceDate from ChainStopper where LooseETypeId='" + id + "'", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }

                    if (damageOb == "Chafe Guard")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutofServiceDate from chafeguard where CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            dtt = dt1.Rows[0][0].ToString();
                        }
                    }
                    if (damageOb == "Winch Brake Test Kit")
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("select OutofServiceDate from WinchBreakTestKit where CertificateNumber not in (select LooseECertiNo from LooseEDisposal)", sc.con);
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
