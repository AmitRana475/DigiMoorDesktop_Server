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
    /// Interaction logic for CrewWorkPlanningView.xaml
    /// </summary>
    public partial class CrewWorkPlanningView : UserControl
    {
        string IMO_Number = "";
        WebBrowser browser;
        List<WorkHoursPlannerClass> reports;
        private readonly ShipmentContaxt sc;
        public CrewWorkPlanningView()
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
                MessageBox.Show("Data Not Available For Selected Criteria!  Please Select The Vessel, Month and Year to Check Report.");
        }

        private void browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            try
            {
                lblLoading.Visibility = Visibility.Visible;
                reports = new List<WorkHoursPlannerClass>();

                var month = cbMonth.SelectedValue.ToString();
                var year = Convert.ToInt32(cbYear.SelectedValue.ToString());
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

                string Col1 = "false", Col2 = "false", Col3 = "false", Col4 = "false", Col5 = "false", Col6 = "false", Col7 = "false", Col8 = "false", Col9 = "false", Col10 = "false", Col11 = "false", Col12 = "false", Col13 = "false", Col14 = "false", Col15 = "false", Col16 = "false", Col17 = "false", Col18 = "false", Col19 = "false", Col20 = "false", Col21 = "false", Col22 = "false", Col23 = "false", Col24 = "false", Col25 = "false", Col26 = "false", Col27 = "false", Col28 = "false", Col29 = "false", Col30 = "false", Col31 = "false", Col32 = "false", Col33 = "false", Col34 = "false", Col35 = "false", Col36 = "false", Col37 = "false", Col38 = "false", Col39 = "false", Col40 = "false", Col41 = "false", Col42 = "false", Col43 = "false", Col44 = "false", Col45 = "false", Col46 = "false", Col47 = "false", Col48 = "false", Col49 = "false", Col50 = "false", Col51 = "false", Col52 = "false", Col53 = "false", Col54 = "false", Col55 = "false", Col56 = "false", Col57 = "false", Col58 = "false", Col59 = "false", Col60 = "false", Col61 = "false", Col62 = "false", Col63 = "false", Col64 = "false", Col65 = "false", Col66 = "false", Col67 = "false", Col68 = "false", Col69 = "false", Col70 = "false", Col71 = "false", Col72 = "false", Col73 = "false", Col74 = "false", Col75 = "false", Col76 = "false", Col77 = "false", Col78 = "false", Col79 = "false", Col80 = "false", Col81 = "false", Col82 = "false", Col83 = "false", Col84 = "false", Col85 = "false", Col86 = "false", Col87 = "false", Col88 = "false", Col89 = "false", Col90 = "false", Col91 = "false", Col92 = "false", Col93 = "false", Col94 = "false", Col95 = "false", Col96 = "false", Col97 = "false", Col98 = "false", Col99 = "false", Col100 = "false";
                string Col101 = "false", Col102 = "false", Col103 = "false", Col104 = "false", Col105 = "false", Col106 = "false", Col107 = "false", Col108 = "false", Col109 = "false", Col110 = "false", Col111 = "false", Col112 = "false", Col113 = "false", Col114 = "false", Col115 = "false", Col116 = "false", Col117 = "false", Col118 = "false", Col119 = "false", Col120 = "false";


                var user = MainViewModelReports._CommonData.GetUserDetail;


              

                reports = sc.WorkHourPlanners.OrderBy(a => a.dates).Where(x => x.UserName == user.UserName && x.Position.Trim() == user.position.Trim() && x.dates.Month == monthInDigit && x.dates.Year == year).ToList();
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

                    var cellcount = reports.Max(x => x.Cellcount);

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


                    var companyname = sc.Vessels.FirstOrDefault();
                    var WatchKeeper = user.WatchKeeper;
                    IMO_Number = companyname.imo.ToString();


                    ReportViewerCrewWorkPlanning.Visible = true;
                    ReportViewerCrewWorkPlanning.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("WorkHoursPlanner", reports);

                    if (user.chkyoungs)
                        this.ReportViewerCrewWorkPlanning.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.WorkHoursYoungPlannerReport.rdlc";
                    else
                        this.ReportViewerCrewWorkPlanning.LocalReport.ReportEmbeddedResource = "WorkShipVersionII.RDLCReport.WorkHoursPlannerReport.rdlc";

                    ReportParameter bbv = new ReportParameter("VesselName", companyname.VesselName);
                    ReportParameter bbv1 = new ReportParameter("Flag", companyname.Flag.ToString());
                    ReportParameter bbv2 = new ReportParameter("Imo", companyname.imo.ToString());
                    ReportParameter bbv3 = new ReportParameter("WatchKeeper", WatchKeeper == true ? "Yes" : "No");
                    ReportParameter bbv4 = new ReportParameter("Manila", reports.FirstOrDefault().RuleNames == "Manila" ? "MLC & Manila" : "MLC");

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



                    ReportViewerCrewWorkPlanning.LocalReport.SetParameters(new ReportParameter[] { bbv, bbv1, bbv2, bbv3, bbv4, rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18, rp19, rp20, rp21, rp22, rp23, rp24, rp25, rp26, rp27, rp28, rp29, rp30, rp31, rp32, rp33, rp34, rp35, rp36, rp37, rp38, rp39, rp40, rp41, rp42, rp43, rp44, rp45, rp46, rp47, rp48, rp49, rp50, rp51, rp52, rp53, rp54, rp55, rp56, rp57, rp58, rp59, rp60, rp61, rp62, rp63, rp64, rp65, rp66, rp67, rp68, rp69, rp70, rp71, rp72, rp73, rp74, rp75, rp76, rp77, rp78, rp79, rp80, rp81, rp82, rp83, rp84, rp85, rp86, rp87, rp88, rp89, rp90, rp91, rp92, rp93, rp94, rp95, rp96, rp97, rp98, rp99, rp100, rp101, rp102, rp103, rp104, rp105, rp106, rp107, rp108, rp109, rp110, rp111, rp112, rp113, rp114, rp115, rp116, rp117, rp118, rp119, rp120 });



                    object repotname = "WorkHoursPlanner-" + companyname.imo + "-" + DateTime.Now.Date.ToString("MMM-yyyy");
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
                //sc.ErrorLog(ex);
                MessageBox.Show(ex.ToString());
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
                    string file = "WorkHourPlanners" + "-" + IMO_Number + "-" + dat;
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
                    string file = "WorkHourPlanners" + "-" + IMO_Number + "-" + dat;
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
    }
}
