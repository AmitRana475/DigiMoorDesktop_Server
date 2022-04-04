using DataBuildingLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WorkShipVersionII.ViewModelReports;

namespace WorkShipVersionII.ViewsReports
{
    /// <summary>
    /// Interaction logic for DepartmentPlanningView.xaml
    /// </summary>
    public partial class DepartmentPlanningView : UserControl
    {
        string IMO_Number = "";
        WebBrowser browser;
        List<WorkHoursPlannerClass> reports;
        private readonly ShipmentContaxt sc;

        public DepartmentPlanningView()
        {
            InitializeComponent();

            if (sc == null)
                sc = new ShipmentContaxt();

            browser = new WebBrowser();
           // browser.Navigate(new Uri("http://somepage.com"));
            browser.Navigating += new NavigatingCancelEventHandler(browser_Navigating);
            browser.Navigated += new NavigatedEventHandler(browser_LoadCompleted);

        }


        private void browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            lblLoading.Visibility = Visibility.Hidden;
            lblLoading2.Visibility = Visibility.Visible;
            if (reports.Count == 0)
                MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Department and Date to Check Report.");
        }

        private void browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            try
            {
                lblLoading.Visibility = Visibility.Visible;
                reports = new List<WorkHoursPlannerClass>();

                var month = cbMonth.SelectedItems.ToList();

                string department = string.Empty;

                foreach (var item in month)
                {
                    if (item.Key != "All")
                    {
                        department += item.Key + ",";
                    }
                }

                department = department.TrimEnd(',');

                string[] dept = department.Split(',');

                var year = cbYear.SelectedDate.ToString();



                //if(dept.Length>1)
                GetDeviationReport(year, dept);


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
                    ReportViewerCrewWorkPlanning.PrintDialog();
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
                    string file = "DepartmentPlanners" + "-" + IMO_Number + "-" + dat;
                    saveFileDialog1.FileName = file + ".xls";
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog1.FileName;

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = ReportViewerCrewWorkPlanning.LocalReport.Render(
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
                    string file = "DepartmentPlanners" + "-" + IMO_Number + "-" + dat;
                    saveFileDialog1.FileName = file + ".pdf";
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        string names = saveFileDialog1.FileName;

                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;

                        byte[] bytes = ReportViewerCrewWorkPlanning.LocalReport.Render(
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

                browser = new WebBrowser();
                browser.Navigate(new Uri("http://somepage.com"));
                browser.Navigating += new NavigatingCancelEventHandler(browser_Navigating);
                browser.Navigated += new NavigatedEventHandler(browser_LoadCompleted);


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void GetDeviationReport(string date, string[] department)
        {
            try
            {
                //0.1397cm, 1.65259cm
                //0.1397cm, 1.1875in

                reports = new List<WorkHoursPlannerClass>();
                reports.Clear();

                string ColNormal = "false", ColYoung = "false", Col1 = "false", Col2 = "false", Col3 = "false", Col4 = "false", Col5 = "false", Col6 = "false", Col7 = "false", Col8 = "false", Col9 = "false", Col10 = "false", Col11 = "false", Col12 = "false", Col13 = "false", Col14 = "false", Col15 = "false", Col16 = "false", Col17 = "false", Col18 = "false", Col19 = "false", Col20 = "false", Col21 = "false", Col22 = "false", Col23 = "false", Col24 = "false", Col25 = "false", Col26 = "false", Col27 = "false", Col28 = "false", Col29 = "false", Col30 = "false", Col31 = "false", Col32 = "false", Col33 = "false", Col34 = "false", Col35 = "false", Col36 = "false", Col37 = "false", Col38 = "false", Col39 = "false", Col40 = "false", Col41 = "false", Col42 = "false", Col43 = "false", Col44 = "false", Col45 = "false", Col46 = "false", Col47 = "false", Col48 = "false", Col49 = "false", Col50 = "false", Col51 = "false", Col52 = "false", Col53 = "false", Col54 = "false", Col55 = "false", Col56 = "false", Col57 = "false", Col58 = "false", Col59 = "false", Col60 = "false", Col61 = "false", Col62 = "false", Col63 = "false", Col64 = "false", Col65 = "false", Col66 = "false", Col67 = "false", Col68 = "false", Col69 = "false", Col70 = "false", Col71 = "false", Col72 = "false", Col73 = "false", Col74 = "false", Col75 = "false", Col76 = "false", Col77 = "false", Col78 = "false", Col79 = "false", Col80 = "false", Col81 = "false", Col82 = "false", Col83 = "false", Col84 = "false", Col85 = "false", Col86 = "false", Col87 = "false", Col88 = "false", Col89 = "false", Col90 = "false", Col91 = "false", Col92 = "false", Col93 = "false", Col94 = "false", Col95 = "false", Col96 = "false", Col97 = "false", Col98 = "false", Col99 = "false", Col100 = "false";
                string Col101 = "false", Col102 = "false", Col103 = "false", Col104 = "false", Col105 = "false", Col106 = "false", Col107 = "false", Col108 = "false", Col109 = "false", Col110 = "false", Col111 = "false", Col112 = "false", Col113 = "false", Col114 = "false", Col115 = "false", Col116 = "false", Col117 = "false", Col118 = "false", Col119 = "false", Col120 = "false";


                string YCol1 = "false", YCol2 = "false", YCol3 = "false", YCol4 = "false", YCol5 = "false", YCol6 = "false", YCol7 = "false", YCol8 = "false", YCol9 = "false", YCol10 = "false", YCol11 = "false", YCol12 = "false", YCol13 = "false", YCol14 = "false", YCol15 = "false", YCol16 = "false", YCol17 = "false", YCol18 = "false", YCol19 = "false", YCol20 = "false", YCol21 = "false", YCol22 = "false", YCol23 = "false", YCol24 = "false", YCol25 = "false", YCol26 = "false", YCol27 = "false", YCol28 = "false", YCol29 = "false", YCol30 = "false", YCol31 = "false", YCol32 = "false", YCol33 = "false", YCol34 = "false", YCol35 = "false", YCol36 = "false", YCol37 = "false", YCol38 = "false", YCol39 = "false", YCol40 = "false", YCol41 = "false", YCol42 = "false", YCol43 = "false", YCol44 = "false", YCol45 = "false", YCol46 = "false", YCol47 = "false", YCol48 = "false", YCol49 = "false", YCol50 = "false", YCol51 = "false", YCol52 = "false", YCol53 = "false", YCol54 = "false", YCol55 = "false", YCol56 = "false", YCol57 = "false", YCol58 = "false", YCol59 = "false", YCol60 = "false", YCol61 = "false", YCol62 = "false", YCol63 = "false", YCol64 = "false", YCol65 = "false", YCol66 = "false", YCol67 = "false", YCol68 = "false", YCol69 = "false", YCol70 = "false", YCol71 = "false", YCol72 = "false", YCol73 = "false", YCol74 = "false", YCol75 = "false", YCol76 = "false", YCol77 = "false", YCol78 = "false", YCol79 = "false", YCol80 = "false", YCol81 = "false", YCol82 = "false", YCol83 = "false", YCol84 = "false", YCol85 = "false", YCol86 = "false", YCol87 = "false", YCol88 = "false", YCol89 = "false", YCol90 = "false", YCol91 = "false", YCol92 = "false", YCol93 = "false", YCol94 = "false", YCol95 = "false", YCol96 = "false", YCol97 = "false", YCol98 = "false", YCol99 = "false", YCol100 = "false";
                string YCol101 = "false", YCol102 = "false", YCol103 = "false", YCol104 = "false", YCol105 = "false", YCol106 = "false", YCol107 = "false", YCol108 = "false", YCol109 = "false", YCol110 = "false", YCol111 = "false", YCol112 = "false", YCol113 = "false", YCol114 = "false", YCol115 = "false", YCol116 = "false", YCol117 = "false", YCol118 = "false", YCol119 = "false", YCol120 = "false";


                var user = MainViewModelReports._CommonData.GetUserDetail;

                var dateTime = Convert.ToDateTime(date);

                List<string> deptb = department.ToList();
                if (deptb.Count > 1)
                    deptb.Add("All");

               
                reports = sc.WorkHourPlanners.OrderBy(a => a.dates).Where(x => deptb.Contains(x.Department) && System.Data.Entity.DbFunctions.TruncateTime(x.dates) == System.Data.Entity.DbFunctions.TruncateTime(dateTime)).ToList();
                reports.ForEach(x => { x.Remarks = !string.IsNullOrEmpty(x.Remarks) ? (x.options + " : " + x.Remarks) : x.Remarks; });
                //reports.ForEach(x => { if (!string.IsNullOrEmpty(x.Remarks)) { x.Remarks = (x.options + " : " + x.Remarks); } else { x.Remarks = x.Remarks; } });

                if (reports.Count == 0)
                {
                    ReportViewerCrewWorkPlanning.LocalReport.DataSources.Clear();
                    this.ReportViewerCrewWorkPlanning.Refresh();
                    this.ReportViewerCrewWorkPlanning.Clear();

                }
                else
                {

                    var bbs1 = reports.Where(p => p.opayoung == false).FirstOrDefault();

                    var cellcount = bbs1 != null ? reports.Where(p => p.opayoung == false).Max(x => x.Cellcount) : 0;

                    var bbs = reports.Where(p => p.opayoung).FirstOrDefault();
                    var cellcount1 = bbs != null ? reports.Where(p => p.opayoung).Max(x => x.Cellcount) : 0;

                    ColNormal = cellcount > 0 ? "true" : "false";
                    ColYoung = cellcount1 > 0 ? "true" : "false";


                    while (cellcount > 0)
                    {
                        if (cellcount == 1)
                            Col1 = "true";
                        if (cellcount == 2)
                            Col2 = "true";
                        if (cellcount == 3)
                            Col3 = "true";
                        if (cellcount == 4)
                            Col4 = "true";
                        if (cellcount == 5)
                            Col5 = "true";
                        if (cellcount == 6)
                            Col6 = "true";
                        if (cellcount == 6)
                            Col6 = "true";
                        if (cellcount == 7)
                            Col7 = "true";
                        if (cellcount == 8)
                            Col8 = "true";
                        if (cellcount == 9)
                            Col9 = "true";
                        if (cellcount == 10)
                            Col10 = "true";
                        if (cellcount == 11)
                            Col11 = "true";
                        if (cellcount == 12)
                            Col12 = "true";
                        if (cellcount == 13)
                            Col13 = "true";
                        if (cellcount == 14)
                            Col14 = "true";
                        if (cellcount == 15)
                            Col15 = "true";
                        if (cellcount == 16)
                            Col16 = "true";
                        if (cellcount == 17)
                            Col17 = "true";
                        if (cellcount == 18)
                            Col18 = "true";
                        if (cellcount == 19)
                            Col19 = "true";
                        if (cellcount == 20)
                            Col20 = "true";

                        if (cellcount == 21)
                            Col21 = "true";
                        if (cellcount == 22)
                            Col22 = "true";
                        if (cellcount == 23)
                            Col23 = "true";
                        if (cellcount == 24)
                            Col24 = "true";
                        if (cellcount == 25)
                            Col25 = "true";
                        if (cellcount == 26)
                            Col26 = "true";
                        if (cellcount == 27)
                            Col27 = "true";
                        if (cellcount == 28)
                            Col28 = "true";
                        if (cellcount == 29)
                            Col29 = "true";
                        if (cellcount == 30)
                            Col30 = "true";

                        if (cellcount == 31)
                            Col31 = "true";
                        if (cellcount == 32)
                            Col32 = "true";
                        if (cellcount == 33)
                            Col33 = "true";
                        if (cellcount == 34)
                            Col34 = "true";
                        if (cellcount == 35)
                            Col35 = "true";
                        if (cellcount == 36)
                            Col36 = "true";
                        if (cellcount == 37)
                            Col37 = "true";
                        if (cellcount == 38)
                            Col38 = "true";
                        if (cellcount == 39)
                            Col39 = "true";
                        if (cellcount == 40)
                            Col40 = "true";

                        if (cellcount == 41)
                            Col41 = "true";
                        if (cellcount == 42)
                            Col42 = "true";
                        if (cellcount == 43)
                            Col43 = "true";
                        if (cellcount == 44)
                            Col44 = "true";
                        if (cellcount == 45)
                            Col45 = "true";
                        if (cellcount == 46)
                            Col46 = "true";
                        if (cellcount == 47)
                            Col47 = "true";
                        if (cellcount == 48)
                            Col48 = "true";
                        if (cellcount == 49)
                            Col49 = "true";
                        if (cellcount == 50)
                            Col50 = "true";

                        if (cellcount == 51)
                            Col51 = "true";
                        if (cellcount == 52)
                            Col52 = "true";
                        if (cellcount == 53)
                            Col53 = "true";
                        if (cellcount == 54)
                            Col54 = "true";
                        if (cellcount == 55)
                            Col55 = "true";
                        if (cellcount == 56)
                            Col56 = "true";
                        if (cellcount == 57)
                            Col57 = "true";
                        if (cellcount == 58)
                            Col58 = "true";
                        if (cellcount == 59)
                            Col59 = "true";
                        if (cellcount == 60)
                            Col60 = "true";

                        if (cellcount == 61)
                            Col61 = "true";
                        if (cellcount == 62)
                            Col62 = "true";
                        if (cellcount == 63)
                            Col63 = "true";
                        if (cellcount == 64)
                            Col64 = "true";
                        if (cellcount == 65)
                            Col65 = "true";
                        if (cellcount == 66)
                            Col66 = "true";
                        if (cellcount == 67)
                            Col67 = "true";
                        if (cellcount == 68)
                            Col68 = "true";
                        if (cellcount == 69)
                            Col69 = "true";
                        if (cellcount == 70)
                            Col70 = "true";

                        if (cellcount == 71)
                            Col71 = "true";
                        if (cellcount == 72)
                            Col72 = "true";
                        if (cellcount == 73)
                            Col73 = "true";
                        if (cellcount == 74)
                            Col74 = "true";
                        if (cellcount == 75)
                            Col75 = "true";
                        if (cellcount == 76)
                            Col76 = "true";
                        if (cellcount == 77)
                            Col77 = "true";
                        if (cellcount == 78)
                            Col78 = "true";
                        if (cellcount == 79)
                            Col79 = "true";
                        if (cellcount == 80)
                            Col80 = "true";

                        if (cellcount == 81)
                            Col81 = "true";
                        if (cellcount == 82)
                            Col82 = "true";
                        if (cellcount == 83)
                            Col83 = "true";
                        if (cellcount == 84)
                            Col84 = "true";
                        if (cellcount == 85)
                            Col85 = "true";
                        if (cellcount == 86)
                            Col86 = "true";
                        if (cellcount == 87)
                            Col87 = "true";
                        if (cellcount == 88)
                            Col88 = "true";
                        if (cellcount == 89)
                            Col89 = "true";
                        if (cellcount == 90)
                            Col90 = "true";

                        if (cellcount == 91)
                            Col91 = "true";
                        if (cellcount == 92)
                            Col92 = "true";
                        if (cellcount == 93)
                            Col93 = "true";
                        if (cellcount == 94)
                            Col94 = "true";
                        if (cellcount == 95)
                            Col95 = "true";
                        if (cellcount == 96)
                            Col96 = "true";
                        if (cellcount == 97)
                            Col97 = "true";
                        if (cellcount == 98)
                            Col98 = "true";
                        if (cellcount == 99)
                            Col99 = "true";
                        if (cellcount == 100)
                            Col100 = "true";

                        if (cellcount == 101)
                            Col101 = "true";
                        if (cellcount == 102)
                            Col102 = "true";
                        if (cellcount == 103)
                            Col103 = "true";
                        if (cellcount == 104)
                            Col104 = "true";
                        if (cellcount == 105)
                            Col105 = "true";
                        if (cellcount == 106)
                            Col106 = "true";
                        if (cellcount == 107)
                            Col107 = "true";
                        if (cellcount == 108)
                            Col108 = "true";
                        if (cellcount == 109)
                            Col109 = "true";
                        if (cellcount == 110)
                            Col110 = "true";

                        if (cellcount == 111)
                            Col111 = "true";
                        if (cellcount == 112)
                            Col112 = "true";
                        if (cellcount == 113)
                            Col113 = "true";
                        if (cellcount == 114)
                            Col114 = "true";
                        if (cellcount == 115)
                            Col115 = "true";
                        if (cellcount == 116)
                            Col116 = "true";
                        if (cellcount == 117)
                            Col117 = "true";
                        if (cellcount == 118)
                            Col118 = "true";
                        if (cellcount == 119)
                            Col119 = "true";
                        if (cellcount == 120)
                            Col120 = "true";


                        cellcount--;
                    }

                    //For the Young seafere.....

                    while (cellcount1 > 0)
                    {
                        if (cellcount1 == 1)
                            YCol1 = "true";
                        if (cellcount1 == 2)
                            YCol2 = "true";
                        if (cellcount1 == 3)
                            YCol3 = "true";
                        if (cellcount1 == 4)
                            YCol4 = "true";
                        if (cellcount1 == 5)
                            YCol5 = "true";
                        if (cellcount1 == 6)
                            YCol6 = "true";
                        if (cellcount1 == 6)
                            YCol6 = "true";
                        if (cellcount1 == 7)
                            YCol7 = "true";
                        if (cellcount1 == 8)
                            YCol8 = "true";
                        if (cellcount1 == 9)
                            YCol9 = "true";
                        if (cellcount1 == 10)
                            YCol10 = "true";
                        if (cellcount1 == 11)
                            YCol11 = "true";
                        if (cellcount1 == 12)
                            YCol12 = "true";
                        if (cellcount1 == 13)
                            YCol13 = "true";
                        if (cellcount1 == 14)
                            YCol14 = "true";
                        if (cellcount1 == 15)
                            YCol15 = "true";
                        if (cellcount1 == 16)
                            YCol16 = "true";
                        if (cellcount1 == 17)
                            YCol17 = "true";
                        if (cellcount1 == 18)
                            YCol18 = "true";
                        if (cellcount1 == 19)
                            YCol19 = "true";
                        if (cellcount1 == 20)
                            YCol20 = "true";

                        if (cellcount1 == 21)
                            YCol21 = "true";
                        if (cellcount1 == 22)
                            YCol22 = "true";
                        if (cellcount1 == 23)
                            YCol23 = "true";
                        if (cellcount1 == 24)
                            YCol24 = "true";
                        if (cellcount1 == 25)
                            YCol25 = "true";
                        if (cellcount1 == 26)
                            YCol26 = "true";
                        if (cellcount1 == 27)
                            YCol27 = "true";
                        if (cellcount1 == 28)
                            YCol28 = "true";
                        if (cellcount1 == 29)
                            YCol29 = "true";
                        if (cellcount1 == 30)
                            YCol30 = "true";

                        if (cellcount1 == 31)
                            YCol31 = "true";
                        if (cellcount1 == 32)
                            YCol32 = "true";
                        if (cellcount1 == 33)
                            YCol33 = "true";
                        if (cellcount1 == 34)
                            YCol34 = "true";
                        if (cellcount1 == 35)
                            YCol35 = "true";
                        if (cellcount1 == 36)
                            YCol36 = "true";
                        if (cellcount1 == 37)
                            YCol37 = "true";
                        if (cellcount1 == 38)
                            YCol38 = "true";
                        if (cellcount1 == 39)
                            YCol39 = "true";
                        if (cellcount1 == 40)
                            YCol40 = "true";

                        if (cellcount1 == 41)
                            YCol41 = "true";
                        if (cellcount1 == 42)
                            YCol42 = "true";
                        if (cellcount1 == 43)
                            YCol43 = "true";
                        if (cellcount1 == 44)
                            YCol44 = "true";
                        if (cellcount1 == 45)
                            YCol45 = "true";
                        if (cellcount1 == 46)
                            YCol46 = "true";
                        if (cellcount1 == 47)
                            YCol47 = "true";
                        if (cellcount1 == 48)
                            YCol48 = "true";
                        if (cellcount1 == 49)
                            YCol49 = "true";
                        if (cellcount1 == 50)
                            YCol50 = "true";

                        if (cellcount1 == 51)
                            YCol51 = "true";
                        if (cellcount1 == 52)
                            YCol52 = "true";
                        if (cellcount1 == 53)
                            YCol53 = "true";
                        if (cellcount1 == 54)
                            YCol54 = "true";
                        if (cellcount1 == 55)
                            YCol55 = "true";
                        if (cellcount1 == 56)
                            YCol56 = "true";
                        if (cellcount1 == 57)
                            YCol57 = "true";
                        if (cellcount1 == 58)
                            YCol58 = "true";
                        if (cellcount1 == 59)
                            YCol59 = "true";
                        if (cellcount1 == 60)
                            YCol60 = "true";

                        if (cellcount1 == 61)
                            YCol61 = "true";
                        if (cellcount1 == 62)
                            YCol62 = "true";
                        if (cellcount1 == 63)
                            YCol63 = "true";
                        if (cellcount1 == 64)
                            YCol64 = "true";
                        if (cellcount1 == 65)
                            YCol65 = "true";
                        if (cellcount1 == 66)
                            YCol66 = "true";
                        if (cellcount1 == 67)
                            YCol67 = "true";
                        if (cellcount1 == 68)
                            YCol68 = "true";
                        if (cellcount1 == 69)
                            YCol69 = "true";
                        if (cellcount1 == 70)
                            YCol70 = "true";

                        if (cellcount1 == 71)
                            YCol71 = "true";
                        if (cellcount1 == 72)
                            YCol72 = "true";
                        if (cellcount1 == 73)
                            YCol73 = "true";
                        if (cellcount1 == 74)
                            YCol74 = "true";
                        if (cellcount1 == 75)
                            YCol75 = "true";
                        if (cellcount1 == 76)
                            YCol76 = "true";
                        if (cellcount1 == 77)
                            YCol77 = "true";
                        if (cellcount1 == 78)
                            YCol78 = "true";
                        if (cellcount1 == 79)
                            YCol79 = "true";
                        if (cellcount1 == 80)
                            YCol80 = "true";

                        if (cellcount1 == 81)
                            YCol81 = "true";
                        if (cellcount1 == 82)
                            YCol82 = "true";
                        if (cellcount1 == 83)
                            YCol83 = "true";
                        if (cellcount1 == 84)
                            YCol84 = "true";
                        if (cellcount1 == 85)
                            YCol85 = "true";
                        if (cellcount1 == 86)
                            YCol86 = "true";
                        if (cellcount1 == 87)
                            YCol87 = "true";
                        if (cellcount1 == 88)
                            YCol88 = "true";
                        if (cellcount1 == 89)
                            YCol89 = "true";
                        if (cellcount1 == 90)
                            YCol90 = "true";

                        if (cellcount1 == 91)
                            YCol91 = "true";
                        if (cellcount1 == 92)
                            YCol92 = "true";
                        if (cellcount1 == 93)
                            YCol93 = "true";
                        if (cellcount1 == 94)
                            YCol94 = "true";
                        if (cellcount1 == 95)
                            YCol95 = "true";
                        if (cellcount1 == 96)
                            YCol96 = "true";
                        if (cellcount1 == 97)
                            YCol97 = "true";
                        if (cellcount1 == 98)
                            YCol98 = "true";
                        if (cellcount1 == 99)
                            YCol99 = "true";
                        if (cellcount1 == 100)
                            YCol100 = "true";

                        if (cellcount1 == 101)
                            YCol101 = "true";
                        if (cellcount1 == 102)
                            YCol102 = "true";
                        if (cellcount1 == 103)
                            YCol103 = "true";
                        if (cellcount1 == 104)
                            YCol104 = "true";
                        if (cellcount1 == 105)
                            YCol105 = "true";
                        if (cellcount1 == 106)
                            YCol106 = "true";
                        if (cellcount1 == 107)
                            YCol107 = "true";
                        if (cellcount1 == 108)
                            YCol108 = "true";
                        if (cellcount1 == 109)
                            YCol109 = "true";
                        if (cellcount1 == 110)
                            YCol110 = "true";

                        if (cellcount1 == 111)
                            YCol111 = "true";
                        if (cellcount1 == 112)
                            YCol112 = "true";
                        if (cellcount1 == 113)
                            YCol113 = "true";
                        if (cellcount1 == 114)
                            YCol114 = "true";
                        if (cellcount1 == 115)
                            YCol115 = "true";
                        if (cellcount1 == 116)
                            YCol116 = "true";
                        if (cellcount1 == 117)
                            YCol117 = "true";
                        if (cellcount1 == 118)
                            YCol118 = "true";
                        if (cellcount1 == 119)
                            YCol119 = "true";
                        if (cellcount1 == 120)
                            YCol120 = "true";


                        cellcount1--;
                    }



                    var companyname = sc.Vessels.FirstOrDefault();
                    //var WatchKeeper = user.WatchKeeper;
                    IMO_Number = companyname.imo.ToString();


                    ReportViewerCrewWorkPlanning.Visible = true;
                    ReportViewerCrewWorkPlanning.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("WorkHoursPlanner", reports);

                    //if (user.chkyoungs)
                    //    ReportViewerCrewWorkPlanning.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.WorkHoursYoungReport.rdlc";
                    //else
                    ReportViewerCrewWorkPlanning.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.DepartmentPlanningReport.rdlc";

                    var deps = string.Join(",", department);
                    string Departmentsign = department.Length > 1 ? "Signature of Master : " : "Signature of HOD : ";

                    ReportParameter bbv = new ReportParameter("VesselName", companyname.VesselName);
                    ReportParameter bbv1 = new ReportParameter("Flag", companyname.Flag.ToString());
                    ReportParameter bbv2 = new ReportParameter("Imo", companyname.imo.ToString());
                    //ReportParameter bbv3 = new ReportParameter("WatchKeeper", WatchKeeper == true ? "Yes" : "No");
                    ReportParameter bbv3 = new ReportParameter("WatchKeeper", "Yes");
                    ReportParameter bbv4 = new ReportParameter("Manila", reports.FirstOrDefault().RuleNames == "Manila" ? "MLC & Manila" : "MLC");
                    ReportParameter bbv5 = new ReportParameter("Department", deps);
                    ReportParameter bbv6 = new ReportParameter("Departmentsign", Departmentsign);//ColNormal

                    ReportParameter ColNrm = new ReportParameter("ColNormal", ColNormal);
                    ReportParameter ColYng = new ReportParameter("ColYoung", ColYoung);

                    ReportParameter rp1 = new ReportParameter("Col1", Col1);
                    ReportParameter rp2 = new ReportParameter("Col2", Col2);
                    ReportParameter rp3 = new ReportParameter("Col3", Col3);
                    ReportParameter rp4 = new ReportParameter("Col4", Col4);
                    ReportParameter rp5 = new ReportParameter("Col5", Col5);
                    ReportParameter rp6 = new ReportParameter("Col6", Col6);
                    ReportParameter rp7 = new ReportParameter("Col7", Col7);
                    ReportParameter rp8 = new ReportParameter("Col8", Col8);
                    ReportParameter rp9 = new ReportParameter("Col9", Col9);
                    ReportParameter rp10 = new ReportParameter("Col10", Col10);

                    ReportParameter rp11 = new ReportParameter("Col11", Col11);
                    ReportParameter rp12 = new ReportParameter("Col12", Col12);
                    ReportParameter rp13 = new ReportParameter("Col13", Col13);
                    ReportParameter rp14 = new ReportParameter("Col14", Col14);
                    ReportParameter rp15 = new ReportParameter("Col15", Col15);
                    ReportParameter rp16 = new ReportParameter("Col16", Col16);
                    ReportParameter rp17 = new ReportParameter("Col17", Col17);
                    ReportParameter rp18 = new ReportParameter("Col18", Col18);
                    ReportParameter rp19 = new ReportParameter("Col19", Col19);
                    ReportParameter rp20 = new ReportParameter("Col20", Col20);

                    ReportParameter rp21 = new ReportParameter("Col21", Col21);
                    ReportParameter rp22 = new ReportParameter("Col22", Col22);
                    ReportParameter rp23 = new ReportParameter("Col23", Col23);
                    ReportParameter rp24 = new ReportParameter("Col24", Col24);
                    ReportParameter rp25 = new ReportParameter("Col25", Col25);
                    ReportParameter rp26 = new ReportParameter("Col26", Col26);
                    ReportParameter rp27 = new ReportParameter("Col27", Col27);
                    ReportParameter rp28 = new ReportParameter("Col28", Col28);
                    ReportParameter rp29 = new ReportParameter("Col29", Col29);
                    ReportParameter rp30 = new ReportParameter("Col30", Col30);

                    ReportParameter rp31 = new ReportParameter("Col31", Col31);
                    ReportParameter rp32 = new ReportParameter("Col32", Col32);
                    ReportParameter rp33 = new ReportParameter("Col33", Col33);
                    ReportParameter rp34 = new ReportParameter("Col34", Col34);
                    ReportParameter rp35 = new ReportParameter("Col35", Col35);
                    ReportParameter rp36 = new ReportParameter("Col36", Col36);
                    ReportParameter rp37 = new ReportParameter("Col37", Col37);
                    ReportParameter rp38 = new ReportParameter("Col38", Col38);
                    ReportParameter rp39 = new ReportParameter("Col39", Col39);
                    ReportParameter rp40 = new ReportParameter("Col40", Col40);

                    ReportParameter rp41 = new ReportParameter("Col41", Col41);
                    ReportParameter rp42 = new ReportParameter("Col42", Col42);
                    ReportParameter rp43 = new ReportParameter("Col43", Col43);
                    ReportParameter rp44 = new ReportParameter("Col44", Col44);
                    ReportParameter rp45 = new ReportParameter("Col45", Col45);
                    ReportParameter rp46 = new ReportParameter("Col46", Col46);
                    ReportParameter rp47 = new ReportParameter("Col47", Col47);
                    ReportParameter rp48 = new ReportParameter("Col48", Col48);
                    ReportParameter rp49 = new ReportParameter("Col49", Col49);
                    ReportParameter rp50 = new ReportParameter("Col50", Col50);

                    ReportParameter rp51 = new ReportParameter("Col51", Col51);
                    ReportParameter rp52 = new ReportParameter("Col52", Col52);
                    ReportParameter rp53 = new ReportParameter("Col53", Col53);
                    ReportParameter rp54 = new ReportParameter("Col54", Col54);
                    ReportParameter rp55 = new ReportParameter("Col55", Col55);
                    ReportParameter rp56 = new ReportParameter("Col56", Col56);
                    ReportParameter rp57 = new ReportParameter("Col57", Col57);
                    ReportParameter rp58 = new ReportParameter("Col58", Col58);
                    ReportParameter rp59 = new ReportParameter("Col59", Col59);
                    ReportParameter rp60 = new ReportParameter("Col60", Col60);

                    ReportParameter rp61 = new ReportParameter("Col61", Col61);
                    ReportParameter rp62 = new ReportParameter("Col62", Col62);
                    ReportParameter rp63 = new ReportParameter("Col63", Col63);
                    ReportParameter rp64 = new ReportParameter("Col64", Col64);
                    ReportParameter rp65 = new ReportParameter("Col65", Col65);
                    ReportParameter rp66 = new ReportParameter("Col66", Col66);
                    ReportParameter rp67 = new ReportParameter("Col67", Col67);
                    ReportParameter rp68 = new ReportParameter("Col68", Col68);
                    ReportParameter rp69 = new ReportParameter("Col69", Col69);
                    ReportParameter rp70 = new ReportParameter("Col70", Col70);

                    ReportParameter rp71 = new ReportParameter("Col71", Col71);
                    ReportParameter rp72 = new ReportParameter("Col72", Col72);
                    ReportParameter rp73 = new ReportParameter("Col73", Col73);
                    ReportParameter rp74 = new ReportParameter("Col74", Col74);
                    ReportParameter rp75 = new ReportParameter("Col75", Col75);
                    ReportParameter rp76 = new ReportParameter("Col76", Col76);
                    ReportParameter rp77 = new ReportParameter("Col77", Col77);
                    ReportParameter rp78 = new ReportParameter("Col78", Col78);
                    ReportParameter rp79 = new ReportParameter("Col79", Col79);
                    ReportParameter rp80 = new ReportParameter("Col80", Col80);

                    ReportParameter rp81 = new ReportParameter("Col81", Col81);
                    ReportParameter rp82 = new ReportParameter("Col82", Col82);
                    ReportParameter rp83 = new ReportParameter("Col83", Col83);
                    ReportParameter rp84 = new ReportParameter("Col84", Col84);
                    ReportParameter rp85 = new ReportParameter("Col85", Col85);
                    ReportParameter rp86 = new ReportParameter("Col86", Col86);
                    ReportParameter rp87 = new ReportParameter("Col87", Col87);
                    ReportParameter rp88 = new ReportParameter("Col88", Col88);
                    ReportParameter rp89 = new ReportParameter("Col89", Col89);
                    ReportParameter rp90 = new ReportParameter("Col90", Col90);

                    ReportParameter rp91 = new ReportParameter("Col91", Col91);
                    ReportParameter rp92 = new ReportParameter("Col92", Col92);
                    ReportParameter rp93 = new ReportParameter("Col93", Col93);
                    ReportParameter rp94 = new ReportParameter("Col94", Col94);
                    ReportParameter rp95 = new ReportParameter("Col95", Col95);
                    ReportParameter rp96 = new ReportParameter("Col96", Col96);
                    ReportParameter rp97 = new ReportParameter("Col97", Col97);
                    ReportParameter rp98 = new ReportParameter("Col98", Col98);
                    ReportParameter rp99 = new ReportParameter("Col99", Col99);
                    ReportParameter rp100 = new ReportParameter("Col100", Col100);

                    ReportParameter rp101 = new ReportParameter("Col101", Col101);
                    ReportParameter rp102 = new ReportParameter("Col102", Col102);
                    ReportParameter rp103 = new ReportParameter("Col103", Col103);
                    ReportParameter rp104 = new ReportParameter("Col104", Col104);
                    ReportParameter rp105 = new ReportParameter("Col105", Col105);
                    ReportParameter rp106 = new ReportParameter("Col106", Col106);
                    ReportParameter rp107 = new ReportParameter("Col107", Col107);
                    ReportParameter rp108 = new ReportParameter("Col108", Col108);
                    ReportParameter rp109 = new ReportParameter("Col109", Col109);
                    ReportParameter rp110 = new ReportParameter("Col110", Col110);

                    ReportParameter rp111 = new ReportParameter("Col111", Col111);
                    ReportParameter rp112 = new ReportParameter("Col112", Col112);
                    ReportParameter rp113 = new ReportParameter("Col113", Col113);
                    ReportParameter rp114 = new ReportParameter("Col114", Col114);
                    ReportParameter rp115 = new ReportParameter("Col115", Col115);
                    ReportParameter rp116 = new ReportParameter("Col116", Col116);
                    ReportParameter rp117 = new ReportParameter("Col117", Col117);
                    ReportParameter rp118 = new ReportParameter("Col118", Col118);
                    ReportParameter rp119 = new ReportParameter("Col119", Col119);
                    ReportParameter rp120 = new ReportParameter("Col120", Col120);



                    // for the Young Seafere...

                    ReportParameter Yrp1 = new ReportParameter("YCol1", YCol1);
                    ReportParameter Yrp2 = new ReportParameter("YCol2", YCol2);
                    ReportParameter Yrp3 = new ReportParameter("YCol3", YCol3);
                    ReportParameter Yrp4 = new ReportParameter("YCol4", YCol4);
                    ReportParameter Yrp5 = new ReportParameter("YCol5", YCol5);
                    ReportParameter Yrp6 = new ReportParameter("YCol6", YCol6);
                    ReportParameter Yrp7 = new ReportParameter("YCol7", YCol7);
                    ReportParameter Yrp8 = new ReportParameter("YCol8", YCol8);
                    ReportParameter Yrp9 = new ReportParameter("YCol9", YCol9);
                    ReportParameter Yrp10 = new ReportParameter("YCol10", YCol10);

                    ReportParameter Yrp11 = new ReportParameter("YCol11", YCol11);
                    ReportParameter Yrp12 = new ReportParameter("YCol12", YCol12);
                    ReportParameter Yrp13 = new ReportParameter("YCol13", YCol13);
                    ReportParameter Yrp14 = new ReportParameter("YCol14", YCol14);
                    ReportParameter Yrp15 = new ReportParameter("YCol15", YCol15);
                    ReportParameter Yrp16 = new ReportParameter("YCol16", YCol16);
                    ReportParameter Yrp17 = new ReportParameter("YCol17", YCol17);
                    ReportParameter Yrp18 = new ReportParameter("YCol18", YCol18);
                    ReportParameter Yrp19 = new ReportParameter("YCol19", YCol19);
                    ReportParameter Yrp20 = new ReportParameter("YCol20", YCol20);

                    ReportParameter Yrp21 = new ReportParameter("YCol21", YCol21);
                    ReportParameter Yrp22 = new ReportParameter("YCol22", YCol22);
                    ReportParameter Yrp23 = new ReportParameter("YCol23", YCol23);
                    ReportParameter Yrp24 = new ReportParameter("YCol24", YCol24);
                    ReportParameter Yrp25 = new ReportParameter("YCol25", YCol25);
                    ReportParameter Yrp26 = new ReportParameter("YCol26", YCol26);
                    ReportParameter Yrp27 = new ReportParameter("YCol27", YCol27);
                    ReportParameter Yrp28 = new ReportParameter("YCol28", YCol28);
                    ReportParameter Yrp29 = new ReportParameter("YCol29", YCol29);
                    ReportParameter Yrp30 = new ReportParameter("YCol30", YCol30);

                    ReportParameter Yrp31 = new ReportParameter("YCol31", YCol31);
                    ReportParameter Yrp32 = new ReportParameter("YCol32", YCol32);
                    ReportParameter Yrp33 = new ReportParameter("YCol33", YCol33);
                    ReportParameter Yrp34 = new ReportParameter("YCol34", YCol34);
                    ReportParameter Yrp35 = new ReportParameter("YCol35", YCol35);
                    ReportParameter Yrp36 = new ReportParameter("YCol36", YCol36);
                    ReportParameter Yrp37 = new ReportParameter("YCol37", YCol37);
                    ReportParameter Yrp38 = new ReportParameter("YCol38", YCol38);
                    ReportParameter Yrp39 = new ReportParameter("YCol39", YCol39);
                    ReportParameter Yrp40 = new ReportParameter("YCol40", YCol40);

                    ReportParameter Yrp41 = new ReportParameter("YCol41", YCol41);
                    ReportParameter Yrp42 = new ReportParameter("YCol42", YCol42);
                    ReportParameter Yrp43 = new ReportParameter("YCol43", YCol43);
                    ReportParameter Yrp44 = new ReportParameter("YCol44", YCol44);
                    ReportParameter Yrp45 = new ReportParameter("YCol45", YCol45);
                    ReportParameter Yrp46 = new ReportParameter("YCol46", YCol46);
                    ReportParameter Yrp47 = new ReportParameter("YCol47", YCol47);
                    ReportParameter Yrp48 = new ReportParameter("YCol48", YCol48);
                    ReportParameter Yrp49 = new ReportParameter("YCol49", YCol49);
                    ReportParameter Yrp50 = new ReportParameter("YCol50", YCol50);

                    ReportParameter Yrp51 = new ReportParameter("YCol51", YCol51);
                    ReportParameter Yrp52 = new ReportParameter("YCol52", YCol52);
                    ReportParameter Yrp53 = new ReportParameter("YCol53", YCol53);
                    ReportParameter Yrp54 = new ReportParameter("YCol54", YCol54);
                    ReportParameter Yrp55 = new ReportParameter("YCol55", YCol55);
                    ReportParameter Yrp56 = new ReportParameter("YCol56", YCol56);
                    ReportParameter Yrp57 = new ReportParameter("YCol57", YCol57);
                    ReportParameter Yrp58 = new ReportParameter("YCol58", YCol58);
                    ReportParameter Yrp59 = new ReportParameter("YCol59", YCol59);
                    ReportParameter Yrp60 = new ReportParameter("YCol60", YCol60);

                    ReportParameter Yrp61 = new ReportParameter("YCol61", YCol61);
                    ReportParameter Yrp62 = new ReportParameter("YCol62", YCol62);
                    ReportParameter Yrp63 = new ReportParameter("YCol63", YCol63);
                    ReportParameter Yrp64 = new ReportParameter("YCol64", YCol64);
                    ReportParameter Yrp65 = new ReportParameter("YCol65", YCol65);
                    ReportParameter Yrp66 = new ReportParameter("YCol66", YCol66);
                    ReportParameter Yrp67 = new ReportParameter("YCol67", YCol67);
                    ReportParameter Yrp68 = new ReportParameter("YCol68", YCol68);
                    ReportParameter Yrp69 = new ReportParameter("YCol69", YCol69);
                    ReportParameter Yrp70 = new ReportParameter("YCol70", YCol70);

                    ReportParameter Yrp71 = new ReportParameter("YCol71", YCol71);
                    ReportParameter Yrp72 = new ReportParameter("YCol72", YCol72);
                    ReportParameter Yrp73 = new ReportParameter("YCol73", YCol73);
                    ReportParameter Yrp74 = new ReportParameter("YCol74", YCol74);
                    ReportParameter Yrp75 = new ReportParameter("YCol75", YCol75);
                    ReportParameter Yrp76 = new ReportParameter("YCol76", YCol76);
                    ReportParameter Yrp77 = new ReportParameter("YCol77", YCol77);
                    ReportParameter Yrp78 = new ReportParameter("YCol78", YCol78);
                    ReportParameter Yrp79 = new ReportParameter("YCol79", YCol79);
                    ReportParameter Yrp80 = new ReportParameter("YCol80", YCol80);

                    ReportParameter Yrp81 = new ReportParameter("YCol81", YCol81);
                    ReportParameter Yrp82 = new ReportParameter("YCol82", YCol82);
                    ReportParameter Yrp83 = new ReportParameter("YCol83", YCol83);
                    ReportParameter Yrp84 = new ReportParameter("YCol84", YCol84);
                    ReportParameter Yrp85 = new ReportParameter("YCol85", YCol85);
                    ReportParameter Yrp86 = new ReportParameter("YCol86", YCol86);
                    ReportParameter Yrp87 = new ReportParameter("YCol87", YCol87);
                    ReportParameter Yrp88 = new ReportParameter("YCol88", YCol88);
                    ReportParameter Yrp89 = new ReportParameter("YCol89", YCol89);
                    ReportParameter Yrp90 = new ReportParameter("YCol90", YCol90);

                    ReportParameter Yrp91 = new ReportParameter("YCol91", YCol91);
                    ReportParameter Yrp92 = new ReportParameter("YCol92", YCol92);
                    ReportParameter Yrp93 = new ReportParameter("YCol93", YCol93);
                    ReportParameter Yrp94 = new ReportParameter("YCol94", YCol94);
                    ReportParameter Yrp95 = new ReportParameter("YCol95", YCol95);
                    ReportParameter Yrp96 = new ReportParameter("YCol96", YCol96);
                    ReportParameter Yrp97 = new ReportParameter("YCol97", YCol97);
                    ReportParameter Yrp98 = new ReportParameter("YCol98", YCol98);
                    ReportParameter Yrp99 = new ReportParameter("YCol99", YCol99);
                    ReportParameter Yrp100 = new ReportParameter("YCol100", YCol100);

                    ReportParameter Yrp101 = new ReportParameter("YCol101", YCol101);
                    ReportParameter Yrp102 = new ReportParameter("YCol102", YCol102);
                    ReportParameter Yrp103 = new ReportParameter("YCol103", YCol103);
                    ReportParameter Yrp104 = new ReportParameter("YCol104", YCol104);
                    ReportParameter Yrp105 = new ReportParameter("YCol105", YCol105);
                    ReportParameter Yrp106 = new ReportParameter("YCol106", YCol106);
                    ReportParameter Yrp107 = new ReportParameter("YCol107", YCol107);
                    ReportParameter Yrp108 = new ReportParameter("YCol108", YCol108);
                    ReportParameter Yrp109 = new ReportParameter("YCol109", YCol109);
                    ReportParameter Yrp110 = new ReportParameter("YCol110", YCol110);

                    ReportParameter Yrp111 = new ReportParameter("YCol111", YCol111);
                    ReportParameter Yrp112 = new ReportParameter("YCol112", YCol112);
                    ReportParameter Yrp113 = new ReportParameter("YCol113", YCol113);
                    ReportParameter Yrp114 = new ReportParameter("YCol114", YCol114);
                    ReportParameter Yrp115 = new ReportParameter("YCol115", YCol115);
                    ReportParameter Yrp116 = new ReportParameter("YCol116", YCol116);
                    ReportParameter Yrp117 = new ReportParameter("YCol117", YCol117);
                    ReportParameter Yrp118 = new ReportParameter("YCol118", YCol118);
                    ReportParameter Yrp119 = new ReportParameter("YCol119", YCol119);
                    ReportParameter Yrp120 = new ReportParameter("YCol120", YCol120);



                    ReportViewerCrewWorkPlanning.LocalReport.SetParameters(new ReportParameter[] { bbv, bbv1, bbv2, bbv3, bbv4, bbv5, bbv6, ColNrm,ColYng, rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18, rp19, rp20, rp21, rp22, rp23, rp24, rp25, rp26, rp27, rp28, rp29, rp30, rp31, rp32, rp33, rp34, rp35, rp36, rp37, rp38, rp39, rp40, rp41, rp42, rp43, rp44, rp45, rp46, rp47, rp48, rp49, rp50, rp51, rp52, rp53, rp54, rp55, rp56, rp57, rp58, rp59, rp60, rp61, rp62, rp63, rp64, rp65, rp66, rp67, rp68, rp69, rp70, rp71, rp72, rp73, rp74, rp75, rp76, rp77, rp78, rp79, rp80, rp81, rp82, rp83, rp84, rp85, rp86, rp87, rp88, rp89, rp90, rp91, rp92, rp93, rp94, rp95, rp96, rp97, rp98, rp99, rp100, rp101, rp102, rp103, rp104, rp105, rp106, rp107, rp108, rp109, rp110, rp111, rp112, rp113, rp114, rp115, rp116, rp117, rp118, rp119, rp120, Yrp1, Yrp2, Yrp3, Yrp4, Yrp5, Yrp6, Yrp7, Yrp8, Yrp9, Yrp10, Yrp11, Yrp12, Yrp13, Yrp14, Yrp15, Yrp16, Yrp17, Yrp18, Yrp19, Yrp20, Yrp21, Yrp22, Yrp23, Yrp24, Yrp25, Yrp26, Yrp27, Yrp28, Yrp29, Yrp30, Yrp31, Yrp32, Yrp33, Yrp34, Yrp35, Yrp36, Yrp37, Yrp38, Yrp39, Yrp40, Yrp41, Yrp42, Yrp43, Yrp44, Yrp45, Yrp46, Yrp47, Yrp48, Yrp49, Yrp50, Yrp51, Yrp52, Yrp53, Yrp54, Yrp55, Yrp56, Yrp57, Yrp58, Yrp59, Yrp60, Yrp61, Yrp62, Yrp63, Yrp64, Yrp65, Yrp66, Yrp67, Yrp68, Yrp69, Yrp70, Yrp71, Yrp72, Yrp73, Yrp74, Yrp75, Yrp76, Yrp77, Yrp78, Yrp79, Yrp80, Yrp81, Yrp82, Yrp83, Yrp84, Yrp85, Yrp86, Yrp87, Yrp88, Yrp89, Yrp90, Yrp91, Yrp92, Yrp93, Yrp94, Yrp95, Yrp96, Yrp97, Yrp98, Yrp99, Yrp100, Yrp101, Yrp102, Yrp103, Yrp104, Yrp105, Yrp106, Yrp107, Yrp108, Yrp109, Yrp110, Yrp111, Yrp112, Yrp113, Yrp114, Yrp115, Yrp116, Yrp117, Yrp118, Yrp119, Yrp120 });

                    object repotname = "DepartmentPlanners-" + companyname.imo + "-" + DateTime.Now.Date.ToString("MMM-yyyy");
                    ReportViewerCrewWorkPlanning.LocalReport.DisplayName = repotname.ToString();

                    ReportViewerCrewWorkPlanning.LocalReport.DataSources.Add(rdc);
                    ReportViewerCrewWorkPlanning.RefreshReport();




                    ReportViewerCrewWorkPlanning.ShowPrintButton = true;
                    ReportViewerCrewWorkPlanning.ShowRefreshButton = true;
                    ReportViewerCrewWorkPlanning.ShowZoomControl = true;
                    ReportViewerCrewWorkPlanning.ShowExportButton = false;

                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


    }
}
