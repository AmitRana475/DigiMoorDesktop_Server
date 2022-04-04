using DataBuildingLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WorkShipVersionII.ViewsReports
{
    /// <summary>
    /// Interaction logic for WorkScheduleView.xaml
    /// </summary>
    public partial class WorkScheduleView : UserControl
    {
       // WebBrowser browser;
        List<CrewDetailClass> reports;
        private readonly ShipmentContaxt sc;
        public WorkScheduleView()
        {
            InitializeComponent();
            if (sc == null)
                sc = new ShipmentContaxt();

            //browser = new WebBrowser();
            //browser.Navigate(new Uri("http://google.com"));
            //browser.Navigating += new NavigatingCancelEventHandler(browser_Navigating);
            //browser.Navigated += new NavigatedEventHandler(browser_LoadCompleted);

            browser_Navigating();
            browser_LoadCompleted();
        }


        void browser_Navigating()
        {
            try
            {
                lblLoading.Visibility = Visibility.Visible;
                ReportLoaded();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        //SECOND EVENT TO FIRE AFTER YOU POST INFORMATION
        void browser_LoadCompleted()
        {

            lblLoading.Visibility = Visibility.Hidden;
            lblLoading2.Visibility = Visibility.Visible;
            if (reports.Count == 0)
                MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Vessel, Month and Year to Check Report.");
        }




        private void ReportLoaded()
        {
            try
            {
                reports = new List<CrewDetailClass>();

                var companyname = sc.Vessels.FirstOrDefault();

                //var checkdate = sc.CrewDetails.Where(x => x.Dates.Month.ToString() == datefrom.ToString() && x.Dates.Year.ToString() == CrewReportLayer.bmsuploaddat.dateto.ToString()).FirstOrDefault();

                //if (checkdate != null)
                //{
                //    reports = (from p in sc.CrewDetails.OrderBy(a => a.Position)
                //               where p.VesselName.ToLower() == CrewReportLayer.bmsuploaddat.vesselname.ToLower()
                //               select p).Distinct().OrderBy(x => x.Position).ToList();
                //}

                reports = sc.CrewDetails.OrderBy(a => a.rid).ToList();
                if (reports.Count == 0)
                {
                    ReportViewerWorkSchedule.LocalReport.DataSources.Clear();
                    ReportViewerWorkSchedule.LocalReport.Refresh();
                    //ReportViewerWorkSchedule.Visible = false;

                }
                else
                {
                    //ReportViewerDemo.Visible = true;
                    //ReportViewerDemo.LocalReport.DataSources.Add(new ReportDataSource("CrewDetail", reports));
                    // ReportViewerDemo.RefreshReport();


                    ReportViewerWorkSchedule.Visible = true;
                    ReportViewerWorkSchedule.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("CrewDetail", reports);
                    ReportViewerWorkSchedule.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.CrewDetailReport.rdlc";

                    ReportParameter bbv = new ReportParameter("VesselName", companyname.VesselName);
                    ReportParameter bbv1 = new ReportParameter("Flag", companyname.Flag.ToString());
                    ReportParameter bbv2 = new ReportParameter("Imo", companyname.imo.ToString());

                    ReportViewerWorkSchedule.LocalReport.SetParameters(new ReportParameter[] { bbv, bbv1, bbv2 });

                    object repotname = "IndividualWorkSchedule " + companyname.VesselName + " " + DateTime.Now.Date.ToString("MMM yy");
                    ReportViewerWorkSchedule.LocalReport.DisplayName = repotname.ToString();

                    ReportViewerWorkSchedule.LocalReport.DataSources.Add(rdc);
                    ReportViewerWorkSchedule.RefreshReport();




                    ReportViewerWorkSchedule.ShowPrintButton = true;
                    ReportViewerWorkSchedule.ShowRefreshButton = true;
                    ReportViewerWorkSchedule.ShowZoomControl = true;
                    ReportViewerWorkSchedule.ShowToolBar = true;

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }





        private void btnPprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportViewerWorkSchedule.PrintDialog();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

                string dat = DateTime.Now.ToString("MMM-yy");
                string file = "IndividualWorkSchedule" + "_" + dat;
                saveFileDialog1.FileName = file + ".xls";
                if (saveFileDialog1.ShowDialog() == true)
                {
                    string filepath = saveFileDialog1.FileName;

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = ReportViewerWorkSchedule.LocalReport.Render(
                        "Excel", null, out mimeType, out encoding, out filenameExtension,
                        out streamids, out warnings);

                    using (FileStream exf = new FileStream(filepath, FileMode.Create))
                    {
                        exf.Write(bytes, 0, bytes.Length);
                        exf.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

                string dat = DateTime.Now.ToString("MMM-yy");
                string file = "IndividualWorkSchedule" + "_" + dat;
                saveFileDialog1.FileName = file + ".pdf";
                if (saveFileDialog1.ShowDialog() == true)
                {
                    string names = saveFileDialog1.FileName;

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = ReportViewerWorkSchedule.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out filenameExtension,
                        out streamids, out warnings);


                    using (FileStream fs = new FileStream(names, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
    }
}
