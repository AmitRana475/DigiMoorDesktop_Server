using DataBuildingLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WorkShipVersionII.Views
{
    /// <summary>
    /// Interaction logic for CertificationExportList.xaml
    /// </summary>
    public partial class CertificationExportList : UserControl
    {

        string IMO_Number = "";
        List<CertificatesClass> reports;
        private readonly ShipmentContaxt sc;
        public CertificationExportList()
        {
            InitializeComponent();

            if (sc == null)
                sc = new ShipmentContaxt();

            lblLoading.Visibility = Visibility.Visible;
            Browser_Navigating();
            // Browser_LoadCompleted();
        }

        private void Browser_LoadCompleted()
        {
            lblLoading.Visibility = Visibility.Hidden;
            lblLoading2.Visibility = Visibility.Visible;
            if (reports.Count == 0)
                MessageBox.Show("Data not available in the database!");
        }

        private void Browser_Navigating()
        {
            try
            {
                lblLoading.Visibility = Visibility.Visible;
                reports = new List<CertificatesClass>();

                reports = sc.Certificates.OrderBy(a => a.SrNo).ToList();
                var companyname = sc.Vessels.FirstOrDefault();
                IMO_Number = companyname.imo.ToString();

                ReportViewerExportList.Visible = true;
                ReportViewerExportList.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSetcertificates", reports);



                ReportViewerExportList.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.CertificatesExportList.rdlc";

                ReportParameter bbv = new ReportParameter("VesselName", companyname.VesselName);
                ReportParameter bbv1 = new ReportParameter("Flag", companyname.Flag.ToString());
                ReportParameter bbv2 = new ReportParameter("Imo", companyname.imo.ToString());


                ReportViewerExportList.LocalReport.SetParameters(new ReportParameter[] { bbv, bbv1, bbv2 });

                object repotname = "CertificateList-" + companyname.imo + "-" + DateTime.Now.Date.ToString("MMM-yyyy");
                ReportViewerExportList.LocalReport.DisplayName = repotname.ToString();

                ReportViewerExportList.LocalReport.DataSources.Add(rdc);
                ReportViewerExportList.RefreshReport();




                ReportViewerExportList.ShowPrintButton = true;
                ReportViewerExportList.ShowRefreshButton = true;
                ReportViewerExportList.ShowZoomControl = true;
                ReportViewerExportList.ShowExportButton = false;


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
            finally
            {
                Browser_LoadCompleted();
            }
        }


        private void BtnPprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (reports.Count > 0)
                    ReportViewerExportList.PrintDialog();
                else
                    MessageBox.Show("Data not available in the database!");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (reports.Count > 0)
                {

                    Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();


                    string dat = DateTime.Now.ToString("MMM-yy");
                    string file = "CertificateList" + "-" + IMO_Number + "-" + dat;
                    saveFileDialog1.FileName = file + ".xls";
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog1.FileName;

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = ReportViewerExportList.LocalReport.Render(
                            "Excel", null, out mimeType, out encoding, out filenameExtension,
                            out streamids, out warnings);

                        using (FileStream exf = new FileStream(filepath, FileMode.Create))
                        {
                            exf.Write(bytes, 0, bytes.Length);
                            exf.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Data not available in the database!");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (reports.Count > 0)
                {
                    Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

                    string dat = DateTime.Now.ToString("MMM-yy");
                    string file = "CertificateList" + "-" + IMO_Number + "-" + dat;
                    saveFileDialog1.FileName = file + ".pdf";
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string names = saveFileDialog1.FileName;

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = ReportViewerExportList.LocalReport.Render(
                            "PDF", null, out mimeType, out encoding, out filenameExtension,
                            out streamids, out warnings);


                        using (FileStream fs = new FileStream(names, FileMode.Create))
                        {
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Data not available in the database!");
                }
            }

            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

    }
}
