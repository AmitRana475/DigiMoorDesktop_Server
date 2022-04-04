using DataBuildingLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WorkShipVersionII.ViewModelReports;

namespace WorkShipVersionII.ViewsReports
{
    /// <summary>
    /// Interaction logic for DeviationsView.xaml
    /// </summary>
    public partial class DeviationsView : UserControl
    {
       // WebBrowser browser;
        List<WorkHoursClass> reports;
        private readonly ShipmentContaxt sc;
        public DeviationsView()
        {
            InitializeComponent();

            if (sc == null)
                sc = new ShipmentContaxt();

            //browser = new WebBrowser();
            ////browser.Navigate(new Uri("0014"));
            //browser.Navigating += new NavigatingCancelEventHandler(browser_Navigating);
            //browser.Navigated += new NavigatedEventHandler(browser_LoadCompleted);
            browser_Navigating();
            browser_LoadCompleted();
        }

        private void browser_LoadCompleted()
        {
            lblLoading.Visibility = Visibility.Hidden;
            lblLoading2.Visibility = Visibility.Visible;
            if (reports.Count == 0)
                MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Vessel, Month and Year to Check Report.");
        }

        private void browser_Navigating()
        {
            try
            {
                lblLoading.Visibility = Visibility.Visible;
                reports = new List<WorkHoursClass>();

                var mon = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                var yea = DateTime.Now.Year;
                var month = cbMonth.SelectedValue == null ? mon : cbMonth.SelectedValue.ToString();
                var year = cbYear.SelectedValue == null ? yea : Convert.ToInt32(cbYear.SelectedValue.ToString());
                int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;

                GetDeviationReport(year, monthInDigit);
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void GetDeviationReport(int year, int monthInDigit)
        {
            try
            {
                var user = MainViewModelReports._CommonData.GetUserDetail;

                reports = sc.WorkHourss.OrderBy(a => a.dates).Where(x => x.UserName == user.UserName && x.Position.Trim() == user.position.Trim() && x.dates.Month == monthInDigit && x.dates.Year == year).ToList();
                reports.ForEach(x => { x.Remarks = !string.IsNullOrEmpty(x.Remarks) ? (x.options + " : " + x.Remarks) : x.Remarks; });
                //reports.ForEach(x => { if (!string.IsNullOrEmpty(x.Remarks)) { x.Remarks = (x.options + " : " + x.Remarks); } else { x.Remarks = x.Remarks; } });

                if (reports.Count == 0)
                {
                    ReportViewerDeviation.LocalReport.DataSources.Clear();
                    this.ReportViewerDeviation.Refresh();
                    this.ReportViewerDeviation.Clear();

                }
                else
                {

                    var companyname = sc.Vessels.FirstOrDefault();
                    var WatchKeeper = user.WatchKeeper;

                    //string exeFolder = Application.StartupPath;
                    //string reportPath = Path.Combine(exeFolder, @"Reports\report.rdlc");

                    ReportViewerDeviation.Visible = true;
                    ReportViewerDeviation.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("WorkHours", reports);
                    //this.ReportViewerDeviation.LocalReport.ReportPath = "../../RDLCReport/DeviationReport.rdlc";
                    ReportViewerDeviation.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.DeviationReport.rdlc";
                    ReportParameter bbv = new ReportParameter("VesselName", companyname.VesselName);
                    ReportParameter bbv1 = new ReportParameter("Flag", companyname.Flag.ToString());
                    ReportParameter bbv2 = new ReportParameter("Imo", companyname.imo.ToString());
                    ReportParameter bbv3 = new ReportParameter("WatchKeeper", WatchKeeper == true ? "Yes" : "No");

                    ReportViewerDeviation.LocalReport.SetParameters(new ReportParameter[] { bbv, bbv1, bbv2, bbv3 });

                    object repotname = "Deviation " + companyname.VesselName + " " + DateTime.Now.Date.ToString("MMM yy");
                    ReportViewerDeviation.LocalReport.DisplayName = repotname.ToString();

                    ReportViewerDeviation.LocalReport.DataSources.Add(rdc);
                    ReportViewerDeviation.RefreshReport();




                    ReportViewerDeviation.ShowPrintButton = true;
                    ReportViewerDeviation.ShowRefreshButton = true;
                    ReportViewerDeviation.ShowZoomControl = true;
                    ReportViewerDeviation.ShowToolBar = true;

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
                if (reports.Count > 0)
                    ReportViewerDeviation.PrintDialog();
                else
                    MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Vessel, Month and Year to Check Report.");
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
                if (reports.Count > 0)
                {

                    Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

                    string dat = DateTime.Now.ToString("MMM-yy");
                    string file = "Deviation" + "_" + dat;
                    saveFileDialog1.FileName = file + ".xls";
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog1.FileName;

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = ReportViewerDeviation.LocalReport.Render(
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
                    MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Vessel, Month and Year to Check Report.");

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
                if (reports.Count > 0)
                {
                    Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                    string dat = DateTime.Now.ToString("MMM-yy");
                    string file = "Deviation" + "_" + dat;
                    saveFileDialog1.FileName = file + ".pdf";
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string names = saveFileDialog1.FileName;

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = ReportViewerDeviation.LocalReport.Render(
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
                    MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Vessel, Month and Year to Check Report.");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //browser = new WebBrowser();
                //browser.Navigate(new Uri("http://somepage.com"));
                //browser.Navigating += new NavigatingCancelEventHandler(browser_Navigating);
                //browser.Navigated += new NavigatedEventHandler(browser_LoadCompleted);
                browser_Navigating();
                browser_LoadCompleted();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
    }
}
